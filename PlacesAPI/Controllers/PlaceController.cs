﻿using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlacesAPI.Code.Classes;
using PlacesAPI.Context;
using PlacesAPI.Data;
using PlacesAPI.Models;
using PlacesAPI.Views.ItemView;
using PlacesAPI.Views.ListView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlacesAPI.Controllers
{
    [EnableCors("AllowMyOrigin")]
    [Route("api/[controller]")]
    [ApiController]
    public class PlaceController : BaseController<Place, TravelContext>
    {
        #region Constructor

        public PlaceController(TravelContext context)
            : base(context) { }

        #endregion Constructor

        #region API

        [HttpGet]
        public async Task<IEnumerable<PlaceListView>> GetItems()
        {
            return await GetViewsAsync<PlaceListView>();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetItem(int id)
        {
            return await GetViewAsync<PlaceItemView>(id);
        }

        #endregion API

        #region Override Abstract Methods

        protected override DataAccess<Place> LoadDataAccess()
        {
            return new DataAccess<Place>(Context, Context.Place);
        }

        protected override Func<int, Place> GetItemFunction()
        {
            return id => Context
                        .Place
                        .FirstOrDefault(x => x.Id == id);
        }

        protected override Func<IEnumerable<Place>> GetItemsFunction()
        {
            return () => Context
                        .Place
                        .AsEnumerable()
                        .OrderBy(x => x.Name);
        }

        protected override Func<Place, bool> GetExistsFunc(int id)
        {
            return x => x.Id == id;
        }

        #endregion Override Abstract Methods
    }
}