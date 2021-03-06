﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LojasCidadao.Models;

namespace LojasCidadao.Controllers
{
    public class EntidadeController : Controller
    {
        SistemaLojasCidadao lista = new SistemaLojasCidadao();
        //
        // GET: /Entidade/

        [Authorize]
        public ActionResult Index()
        {
            var entidades = lista.getTodasEntidades();
            return View("Index",entidades);
        }

        [Authorize]
        public ActionResult Procura()
        {
            return View("Procura");
        }

        [Authorize]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Procura(FormCollection formValues)
        {
            var nome = Request.Form["Nome"];
            var sigla = Request.Form["Sigla"];
            var entidades = new List<Entidade>();
            try
            {   
                if(!String.IsNullOrEmpty(nome))
                {
                    entidades = lista.getEntidadesPorNome(nome);
                    return View("ResultadosProcura", entidades);
                }
                else if (!String.IsNullOrEmpty(sigla))
                {
                    entidades = lista.getEntidadesPorSigla(sigla);
                    return View("ResultadosProcura", entidades);
                }
                else
                {
                    return View("ResultadosProcura", entidades);
                }
            }
            catch
            {
                return View("ResultadosProcura", entidades);
            }
        }

        [Authorize]
        public ActionResult Detalhes(String id)
        {
            try
            {
                int id_e = Convert.ToInt32(id);
                Entidade e = lista.getEntidadePorID(id_e);
                if (e == null)
                {
                    return View("NotFound");
                }
                else return View(e);
            }
            catch
            {
                return View("Error");
            }
        }

        [Authorize]
        public ActionResult Edit(String id)
        {
            try
            {
                int id_e = Convert.ToInt32(id);
                Entidade e = lista.getEntidadePorID(id_e);
                if (e == null)
                {
                    return View("NotFound");
                }
                else return View(e);
            }
            catch
            {
                return View("Error");
            }
        }

        [Authorize]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(int id, FormCollection formValues)
        {
            Entidade e = lista.getEntidadePorID(id);
            e.setNome(Request.Form["Nome"]);
            e.setSigla(Request.Form["Sigla"]);
            e.setMorada(Request.Form["Morada"]);
            e.setTelefone(Request.Form["Telefone"]);
            e.setFax(Request.Form["Fax"]);
            e.setEmail(Request.Form["Email"]);
            e.setUrl(Request.Form["URL"]);
            e.setSite(Request.Form["Site"]);
            if (e.IsValid)
            {
                lista.updateEntidade(e);
                return RedirectToAction("Index");
            }
            else
            {
                var errors = e.GetRuleViolations();
                foreach (var issue in errors)
                {
                    ModelState.AddModelError(issue.PropertyName, issue.ErrorMessage);
                }
                return View(e);
            }
        }

        [Authorize]
        public ActionResult Create()
        {
            Entidade e = new Entidade();
            return View(e);
        }

        [Authorize]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create(Entidade e)
        {
            try
            {
                e.setNome(Request.Form["Nome"]);
                e.setSigla(Request.Form["Sigla"]);
                e.setMorada(Request.Form["Morada"]);
                e.setTelefone(Request.Form["Telefone"]);
                e.setFax(Request.Form["Fax"]);
                e.setEmail(Request.Form["Email"]);
                e.setUrl(Request.Form["URL"]);
                e.setSite(Request.Form["Site"]);
                if (e.IsValid)
                {
                    lista.addEntidade(e);
                    return RedirectToAction("Index");
                }
                else
                {
                    var errors = e.GetRuleViolations();
                    foreach (var issue in errors)
                    {
                        ModelState.AddModelError(issue.PropertyName, issue.ErrorMessage);
                    }
                    return View(e);
                }
            }
            catch
            {
                return RedirectToAction("Create");
            }
        }

        [Authorize]
        public ActionResult Delete(String id)
        {
            try
            {
                int id_e = Convert.ToInt32(id);
                Entidade e = lista.getEntidadePorID(id_e);
                if (e == null)
                {
                    return View("NotFound");
                }
                else return View(e);
            }
            catch
            {
                return View("Error");
            }
        }

        [Authorize]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Delete(int id, string confirmButton)
        {
            Entidade e = lista.getEntidadePorID(id);
            lista.deleteEntidade(e);
            return View("Deleted");
        }
    }
}
