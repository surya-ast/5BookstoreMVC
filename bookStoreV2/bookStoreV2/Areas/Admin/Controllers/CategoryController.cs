using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bookStoreV2.DataAccess.Repository.IRepository;
using bookStoreV2.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace bookStoreV2.Areas.Admin.Controllers
{

    [Area("Admin")]
    public class CategoryController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(int? id)
        {
            Category category = new Category();
            if (id == null)
            {
                //untuk create
                return View(category);
            }
            // untuk update
            category = _unitOfWork.Category.Get(id.GetValueOrDefault());
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }


        // kita akan menampilkan daftar/list dari category menggunakan data tabels
        // untuk load data dari category dg DataTabels maka perlu API dan java script, spt praktik utk DataTabels

        // disini kita akan membuat API CALLS di dalam controller
        // dengan ASP.NET Core kita bisa menambahkan code di controller code utk views action dan juga API CALLS
        // TAPI KITA TIDAK BISA MELAKUKANNYA JIKA MENGGUNAKAN RAZOR PAGES
        // Kita harus memisahkan controller dan logic dg Razor Pages

        // NAMUUN,, kita bisa mendapatkan keuntungan dg Model MVC Application
        // kita tambahkan region untuk mengisolasi API CALLS

        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            var allObj = _unitOfWork.Category.GetAll();
            return Json(new { data = allObj });
        }

        #endregion
    }
}
