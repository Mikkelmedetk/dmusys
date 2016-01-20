using bootstraptestless.Models;
using bootstraptestless.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using bootstraptestless.Helpers;
using System.IO;
using System.Net;

namespace bootstraptestless.Controllers
{
    public class HomeController : BaseController
    {
        private DataContext _context = new DataContext();
        [Route("Home")]
        [Route("{valgtFag}")]
        [Route("{valgtFag}/{valgtSemester:int}")]
        [Route("{valgtFag}/{valgtSemester:int}/{valgtLektion:int}")]
        [Authorize]
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)] //Denne action tager både imod Get og Post
        public ActionResult Index(string valgtFag, int? valgtSemester, int? valgtLektion, HomeViewModel hvmodel)
        {
            HomeViewModel hvm = new HomeViewModel();
            if (valgtFag == null && HttpContext.Request.HttpMethod == "GET")
            {
                valgtFag = _context._Fag.OrderBy(f => f.Fagnavn).First().friendlyFagNavn;
            }

            else if (HttpContext.Request.HttpMethod == "POST")
            {
                valgtFag = hvmodel.valgtFag.friendlyFagNavn;
            }


            if (valgtSemester == null)
            {
                try
                {
                    valgtSemester = _context._Fag.FirstOrDefault(f => f.friendlyFagNavn == valgtFag).Semester.OrderByDescending(s => s.Id).Select(s => s.Id).First();
                }
                catch (NullReferenceException)
                {

                    return new HttpNotFoundResult();
                }

            }




            //Sætter henter det valgte fag fra parameteren

            try
            {
                hvm.valgtFag = _context._Fag.FirstOrDefault(f => f.friendlyFagNavn == valgtFag);
            }
            catch (NullReferenceException)
            {


            }


            //Henter data udfra det fag man har valgt, og finder ud fra 'valgtSemester' parameteren de data om det valgteSemester 
            try
            {
                hvm.valgtSemester = hvm.valgtFag.Semester.FirstOrDefault(s => s.Id == valgtSemester);
            }
            catch (Exception)
            {

                return new HttpNotFoundResult();
            }


            //Henter lektionerne fra det fag og det semester man har valgt
            try
            {
                hvm.relateredeLektioner = _context._Lektion.Where(l => l.SemesterId == hvm.valgtSemester.Id).Include(m => m.Lektionsbesvarelser).Include(m => m.Kodebesvarelser).ToList();
            }
            catch (Exception)
            {

                return new HttpNotFoundResult();
            }


            if (valgtLektion == null)
            {
                if (hvm.valgtSemester != null && hvm.valgtSemester.Lektioner.Select(l => l.Id).Count() > 0)
                {
                    valgtLektion = hvm.valgtSemester.Lektioner.Select(l => l.Id).Last();
                }
            }

            hvm.valgtLektion = _context._Lektion.Where(l => l.Id == valgtLektion).Include(m => m.Lektiontags).FirstOrDefault();


            if (hvm.valgtLektion != null && _context._Lektionsfiler.Where(lf => lf.LektionsId == hvm.valgtLektion.Id).Count() > 0)
            {
                hvm.lektionsfiler = _context._Lektionsfiler.Where(lf => lf.LektionsId == hvm.valgtLektion.Id).ToList();
            }

            if (hvm.valgtLektion != null && _context._Lektionsbesvarelser.Where(lf => lf.LektionsId == hvm.valgtLektion.Id).Count() > 0)
            {
                hvm.lektionsbesvarelser = _context._Lektionsbesvarelser.Where(lf => lf.LektionsId == hvm.valgtLektion.Id).ToList();
            }

            if (hvm.valgtLektion != null && _context._Kode.Where(kb => kb.LektionsId == hvm.valgtLektion.Id).Count() > 0)
            {
                hvm.kodebesvarelser = _context._Kode.Where(kb => kb.LektionsId == hvm.valgtLektion.Id).ToList();
            }

            //Indsætter data i dropdownlisten
            hvm.ddlSemestre = hvm.valgtFag.Semester.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Navn
            }).OrderByDescending(x => x.Value);

            return View(hvm);
        }


        [Route("opretLektion")]
        [HttpPost]
        public ActionResult opretLektion(HomeViewModel hvm, string fagnavn, int semesterid)
        {

            var semester = _context._Semester.Find(semesterid);
            if(hvm.lektiontags != null)
            {
            foreach (var item in hvm.lektiontags)
            {
                if (tagExist(item.tagName))
                {
                    var exisitingTag = _context._Tags.First(t => t.tagName == item.tagName);
                    exisitingTag.Lektioner.Add(hvm.opretLektion);
                    hvm.opretLektion.Lektiontags.Add(exisitingTag);
                }
                else
                {
                    hvm.opretLektion.Lektiontags.Add(item);
                    item.Lektioner.Add(hvm.opretLektion);
                }
                }

            }

            semester.Lektioner.Add(hvm.opretLektion);
            Success(string.Format("<b>Lektion {0}</b> er blevet tilføjet semesteret", hvm.opretLektion.Lektionnummer), true);
            _context._Semester.Attach(semester);
            _context.SaveChanges();



            return Json(Url.Action("Index", "Home", new { valgtFag = fagnavn, valgtSemester = semesterid, valgtLektion = hvm.opretLektion.Id }));
        }

        [Route("redigerLektion")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult redigerLektion(HomeViewModel hvm, string fagnavn, int semesterid, int lektionsId)
        {

            var lektion = _context._Lektion.Find(lektionsId);

            lektion.Lektionafholdelsestid = hvm.opretLektion.Lektionafholdelsestid;
            lektion.Lektionsbeskrivelse = hvm.opretLektion.Lektionsbeskrivelse;
            _context.SaveChanges();
            Success(string.Format("<b>Lektion {0}</b> er blevet redigereret", hvm.opretLektion.Lektionnummer), true);



            return RedirectToAction("Index", "Home", new { valgtFag = fagnavn, valgtSemester = semesterid, valgtLektion = lektionsId });
        }

        [Route("opretKodebesvarelse")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult opretKodebesvarelse(HomeViewModel hvm, string fagnavn, int semesterid, int inLektionsid)
        {

            var lektion = _context._Lektion.Find(inLektionsid);
            lektion.Kodebesvarelser.Add(hvm.opretKodebesvarelse);
            Success(string.Format("<b>Kodebesvarelsen {0}</b> er blevet tilføjet lektionen", hvm.opretKodebesvarelse.Opgavenavn), true);
            _context._Lektion.Attach(lektion);
            _context.SaveChanges();


            return RedirectToAction("Index", "Home", new { valgtFag = fagnavn, valgtSemester = semesterid, valgtLektion = inLektionsid });
        }

        [Route("Home/deleteKodebesvarelse")]
        [HttpPost]
        public ActionResult deleteKodebesvarelse(int kodeid)
        {
            var kodebesvarelse = _context._Kode.Find(kodeid);
            _context._Kode.Remove(kodebesvarelse);
            _context.SaveChanges();
            return View();
        }

        [ValidateInput(false)]
        [Route("KodeView/{lektionsid}/{kodeid}/{edit}")]
        [HttpGet]
        public ActionResult KodeView(int lektionsid, int kodeid, bool edit)
        {
            KodebesvarelseViewModel kvm = new KodebesvarelseViewModel();

            kvm.lektion = _context._Lektion.FirstOrDefault(l => l.Id == lektionsid);
            kvm.KodeBesvarelse = _context._Kode.FirstOrDefault(k => k.Id == kodeid);
            kvm.Edit = edit;

            return View(kvm);
        }

        [HttpPost]
        [Route("KodeView")]
        public ActionResult KodeView(KodebesvarelseViewModel kvm)
        {

            try
            {
                var model = _context._Kode.Find(kvm.KodeBesvarelse.Id);
                model.Indhold = kvm.KodeBesvarelse.Indhold;
                _context.SaveChanges();
                return Json(new
                {
                    msg = "Er blevet redigereret " + model.Opgavenavn
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [HttpPost]
        [Route("Home/SaveUploadedFile")]
        public ActionResult SaveUploadedFile()
        {
            bool isSavedSuccessfully = true;
            string fName = "";
            int valgtLektionid = int.Parse(Request.Form["valgtLektion"]);
            var valgtLektion = _context._Lektion.Find(valgtLektionid);

            List<Lektionsfiler> colLektionsfiler = new List<Lektionsfiler>();

            try
            {

                foreach (string fileName in Request.Files)
                {
                    HttpPostedFileBase file = Request.Files[fileName];

                    //Save file content goes here
                    fName = file.FileName;
                    if (file != null && file.ContentLength > 0)
                    {

                        var originalDirectory = new DirectoryInfo(string.Format("{0}uploadcontent\\", Server.MapPath(@"\")));

                        string pathString = System.IO.Path.Combine(originalDirectory.ToString(), Request.Form["valgtFag"] + "\\" + Request.Form["valgtSemester"] + "\\Lektion" + Request.Form["Lektionsnummer"] + "\\" + Request.Form["Lektionstype"]);

                        var fileName1 = Path.GetFileName(file.FileName);

                        bool isExists = System.IO.Directory.Exists(pathString);

                        if (!isExists)
                            System.IO.Directory.CreateDirectory(pathString);

                        var path = string.Format("{0}\\{1}", pathString, file.FileName);
                        file.SaveAs(path);

                        Lektionsfiler lektionsfil = new Lektionsfiler { Filnavn = fileName1, Url = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~")) + "uploadcontent/" + Request.Form["valgtFag"] + "/" + Request.Form["valgtSemester"] + "/Lektion" + Request.Form["Lektionsnummer"] + "/" + Request.Form["Lektionstype"] + "/" + fileName1 };

                        colLektionsfiler.Add(lektionsfil);
                    }
                }
            }
            catch (Exception ex)
            {
                isSavedSuccessfully = false;
            }

            foreach (var item in colLektionsfiler)
            {
                valgtLektion.Lektionsfiler.Add(item);
            }

            _context._Lektion.Attach(valgtLektion);
            _context.SaveChanges();


            if (isSavedSuccessfully)
            {
                return Json(new { Message = fName });
            }
            else
            {
                return Json(new { Message = "Error in saving file" });
            }

        }

        [HttpPost]
        [Route("Home/SaveUploadedFileBesvarelse")]
        public ActionResult SaveUploadedFileBesvarelse()
        {
            bool isSavedSuccessfully = true;
            string fName = "";
            int valgtLektionid = int.Parse(Request.Form["valgtLektion"]);
            var valgtLektion = _context._Lektion.Find(valgtLektionid);

            List<Lektionsbesvarelser> colLektionsfiler = new List<Lektionsbesvarelser>();

            try
            {

                foreach (string fileName in Request.Files)
                {
                    HttpPostedFileBase file = Request.Files[fileName];

                    //Save file content goes here
                    fName = file.FileName;
                    if (file != null && file.ContentLength > 0)
                    {

                        var originalDirectory = new DirectoryInfo(string.Format("{0}uploadcontent\\", Server.MapPath(@"\")));

                        string pathString = System.IO.Path.Combine(originalDirectory.ToString(), Request.Form["valgtFag"] + "\\" + Request.Form["valgtSemester"] + "\\Lektion" + Request.Form["Lektionsnummer"] + "\\" + Request.Form["Lektionstype"]);

                        var fileName1 = Path.GetFileName(file.FileName);

                        bool isExists = System.IO.Directory.Exists(pathString);

                        if (!isExists)
                            System.IO.Directory.CreateDirectory(pathString);

                        var path = string.Format("{0}\\{1}", pathString, file.FileName);
                        file.SaveAs(path);

                        Lektionsbesvarelser lektionsfil = new Lektionsbesvarelser { Filnavn = fileName1, Url = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~")) + "uploadcontent/" + Request.Form["valgtFag"] + "/" + Request.Form["valgtSemester"] + "/Lektion" + Request.Form["Lektionsnummer"] + "/" + Request.Form["Lektionstype"] + "/" + fileName1 };

                        colLektionsfiler.Add(lektionsfil);
                    }
                }
            }
            catch (Exception ex)
            {
                isSavedSuccessfully = false;
            }

            foreach (var item in colLektionsfiler)
            {
                valgtLektion.Lektionsbesvarelser.Add(item);
            }

            _context._Lektion.Attach(valgtLektion);
            _context.SaveChanges();


            if (isSavedSuccessfully)
            {
                return Json(new { Message = fName });
            }
            else
            {
                return Json(new { Message = "Error in saving file" });
            }

        }

        [HttpPost]
        [Route("Home/deleteLink")]
        public ActionResult deleteLink(int linkId, string filename, string linktype)
        {
            if (linktype.Equals("Lektionsfil"))
            {
                Lektionsfiler lektionsfiler = _context._Lektionsfiler.Find(linkId);
                _context._Lektionsfiler.Remove(lektionsfiler);
                _context.SaveChanges();
            }
            else if (linktype.Equals("Lektionsbesvarelse"))
            {
                Lektionsbesvarelser lektionsbesvarelse = _context._Lektionsbesvarelser.Find(linkId);
                _context._Lektionsbesvarelser.Remove(lektionsbesvarelse);
                _context.SaveChanges();
            }

            var file = Directory.GetFiles(string.Format("{0}uploadcontent\\", Server.MapPath(@"\")), filename, SearchOption.AllDirectories)
                    .FirstOrDefault();
            if (file == null)
            {

            }
            else
            {
                System.IO.File.Delete(file);
            }

            return View();
        }

        public bool tagExist(string Tag)
        {
            bool tagIsExisting = false;

            if (_context._Tags.Any(t => t.tagName == Tag))
            {
                tagIsExisting = true;
            }

            return tagIsExisting;
        }

        [Route("connectTagToLektion")]
        [HttpPost]
        public ActionResult connectTagToLektion(HomeViewModel hvm, int valgtLektion)
        {
            if(tagExist(hvm.opretTag.tagName)) {
                Tag tag = _context._Tags.SingleOrDefault(t => t.tagName == hvm.opretTag.tagName);
                var lektion = _context._Lektion.Find(valgtLektion);
                tag.Lektioner.Add(lektion);
                _context._Tags.Attach(tag);
                _context.SaveChanges();

                return Json(HttpStatusCode.OK);
            } else
            {
                Tag tag = _context._Tags.Add(hvm.opretTag);
                _context._Lektion.FirstOrDefault(l => l.Id == valgtLektion).Lektiontags.Add(tag);
                _context.SaveChanges();
                return Json(HttpStatusCode.OK);
            }
        }

        [Route("removeTagFromLektion")]
        [HttpPost]
        public ActionResult removeTagFromLektion(HomeViewModel hvm, int valgtLektion)
        {

            var tag = _context._Tags.Include(l => l.Lektioner).FirstOrDefault(t => t.tagName == hvm.opretTag.tagName);

            var lektion =_context._Lektion.Include(t => t.Lektiontags).FirstOrDefault(l => l.Id == valgtLektion);
            lektion.Lektiontags.Remove(tag);
            _context.SaveChanges();

            if(tag.Lektioner.Count() == 0)
            {
                _context._Tags.Remove(tag);
                _context.SaveChanges();
            }

            return Json(HttpStatusCode.OK);
        }


    }

}