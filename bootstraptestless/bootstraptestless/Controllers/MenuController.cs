using bootstraptestless.Models;
using bootstraptestless.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace bootstraptestless.Controllers
{
    
    public class MenuController : Controller
    {
        private DataContext _context = new DataContext();
        // GET: Menu
        [Route("Menu/getMenu")]
        public ActionResult getMenu()
        {
            MenuViewModel mvm = new MenuViewModel();
            mvm.alleFag = _context._Fag.OrderBy(f => f.Fagnavn).ToList();
            return View(mvm);
        }
    }
}