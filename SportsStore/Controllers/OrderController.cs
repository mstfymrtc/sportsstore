﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;

namespace SportsStore.Controllers
{
    
    public class OrderController : Controller
    {
        private IOrderRepository repository;
        private Cart cart;
        public OrderController(IOrderRepository repoService,Cart cartService)
        {

            repository = repoService;
            cart = cartService;
            
        }
        
        // GET
        public ViewResult Checkout()
        {
            return View(new Order());
        }

        //POST
        [HttpPost]
        public IActionResult Checkout(Order order)
        {

            if (cart.Lines.Count()==0)
            {
                ModelState.AddModelError("", "Sorry, your cart is empty. " +
                    "Add some stuff to your cart to checkout!");
            }
            if (ModelState.IsValid)
            {
                order.Lines = cart.Lines.ToArray();
                repository.SaveOrder(order);
                return RedirectToAction(nameof(Completed));
            }
            else
            {
                return View(order);
            }





        }

        public ViewResult Completed()
        {
            cart.Clear();
            return View();
        }
    }
}