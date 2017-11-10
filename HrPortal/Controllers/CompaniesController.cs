﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HrPortal.Services;
using HrPortal.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HrPortal.Controllers
{
    public class CompaniesController : Controller
    {
        private IRepository<Company> companyRepository;
        private IRepository<Location> locationRepository;

        public CompaniesController(IRepository<Company> companyRepository,IRepository<Location> locationRepository)
        {
            this.companyRepository = companyRepository;
            this.locationRepository = locationRepository;
        }
        public IActionResult Index()
        {
            var companys = companyRepository.GetAll("Jobs","Location");
            return View(companys);
        }

        public IActionResult Details(string id)
        {
            var comp = companyRepository.Get(id, "Jobs", "Location");
            return View(comp);
        }
        public IActionResult Create()
        {
            var compa = new Company();
            ViewBag.Companies = new SelectList(companyRepository.GetAll().OrderBy(c => c.Name).ToList(), "Id", "Name");
            ViewBag.Locations = locationRepository.GetAll().OrderBy(l => l.Name).ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Create(Company company)
        {
            if (ModelState.IsValid)
            {
               companyRepository.Insert(company);
                return RedirectToAction("SuccessfullyCreated");
            }
            ViewBag.Companies = new SelectList(companyRepository.GetAll().OrderBy(c => c.Name).ToList(), "Id", "Name");
            ViewBag.Locations = locationRepository.GetAll().OrderBy(l => l.Name).ToList();
            return View(company);
        }

    }
}