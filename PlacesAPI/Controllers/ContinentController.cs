﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlacesAPI.Code.Classes;
using PlacesAPI.Context;
using PlacesAPI.Data;
using PlacesAPI.Models;
using PlacesAPI.Views.ItemView;
using PlacesAPI.Views.ListView;

namespace PlacesAPI.Controllers
{
    [EnableCors("AllowMyOrigin")]
    [Route("api/[controller]")]
    [ApiController]
    public class ContinentController : BaseController<Continent, TravelContext>
    {
        #region Constructor

        public ContinentController(TravelContext context)
            : base(context) { }

        #endregion Constructor

        #region API

        [HttpGet]
        public async Task<IEnumerable<ContinentListView>> GetItems()
        {
            return await GetViewsAsync<ContinentListView>();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetItem(int id)
        {
            return await GetViewAsync<ContinentItemView>(id);
        }

        #endregion API

        #region Override Abstract Methods

        protected override DataAccess<Continent> LoadDataAccess()
        {
            return new DataAccess<Continent>(Context, Context.Continent);
        }

        protected override Func<int, Continent> GetItemFunction()
        {
            return id => Context
                        .Continent
                        .Include(x => x.Children)
                            .ThenInclude(x => x.Territories)
                                .ThenInclude(x => x.Flag)
                        .Include(x => x.Children)
                            .ThenInclude(x => x.Territories)
                                .ThenInclude(x => x.TerritoryType)
                        .Include(x => x.Children)
                            .ThenInclude(x => x.Territories)
                                .ThenInclude(x => x.Children)
                                    .ThenInclude(x => x.TerritoryPlaces)
                                        .ThenInclude(x => x.Place)
                        .Include(x => x.Territories)
                            .ThenInclude(x => x.Flag)
                        .Include(x => x.Territories)
                            .ThenInclude(x => x.TerritoryType)
                        
                        .FirstOrDefault(x => x.Id == id);




            //.Include(x => x.Parent)
            //    .ThenInclude(x => x.Territories)

            //.ThenInclude(x => x.Parent)
            //   
            //        
           
            
            //
            //    .ThenInclude(x => x.Parent)
            //.Include(x => x.Territories)
            //    //

        }

        protected override Func<IEnumerable<Continent>> GetItemsFunction()
        {
            return () => Context
                        .Continent
                        .Include(x => x.Territories)
                        .AsEnumerable()
                        .OrderBy(x => x.Name);
        }

        protected override Func<Continent, bool> GetExistsFunc(int id)
        {
            return x => x.Id == id;
        }

        #endregion Override Abstract Methods
    }
}