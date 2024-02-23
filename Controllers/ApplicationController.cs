using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Threading.Tasks;
using Porfolio6_1.Models;
using System.Web.Http.Controllers;

namespace Porfolio6_1.Controllers
{
    public class ApplicationController : ApiController
    {
        App_Data.ShoeInventoryDBDataClassesDataContext shoeInventoryDBDataClassesDataContext = new App_Data.ShoeInventoryDBDataClassesDataContext();


        public List<ShoeInventoryTableModel> MethodGetAllListShoeInventory()
        {
            List<ShoeInventoryTableModel> listShoeInventoryTableModel = new List<ShoeInventoryTableModel>();

            listShoeInventoryTableModel = (from shoeInventoryTableModel in shoeInventoryDBDataClassesDataContext.ShoeInventoryTables
                                           select new ShoeInventoryTableModel()
                                           {
                                               id = Convert.ToInt32(shoeInventoryTableModel.Id),
                                               shoeName = shoeInventoryTableModel.ShoeName.ToString(),
                                               shoeDescription = shoeInventoryTableModel.ShoeDescription.ToString(),
                                               shoePrice = shoeInventoryTableModel.ShoePrice.ToString(),
                                               shoeSize = Convert.ToInt32(shoeInventoryTableModel.ShoeSize),
                                               quantity = Convert.ToInt32(shoeInventoryTableModel.Quantity)
                                           }).ToList<ShoeInventoryTableModel>();

            return listShoeInventoryTableModel;
        }


        [HttpGet]
        [Route("api/Application/getAllListShoeInventory")]
        public async Task<IHttpActionResult> getAllListShoeInventory()
        {
            Task<List<ShoeInventoryTableModel>> TaskGetAllListShoeInventory = Task.Run(MethodGetAllListShoeInventory);
            await TaskGetAllListShoeInventory;

            if (TaskGetAllListShoeInventory.Result.Count == 0)
            {
                return NotFound();
            }
            return Ok(TaskGetAllListShoeInventory.Result);
        }


        [HttpGet]
        [Route("api/Application/getAllListShoeInventory2")]
        public async Task<IHttpActionResult> getAllListShoeInventory2()
        {
            List<ShoeInventoryTableModel> listShoeInventoryTableModel = new List<ShoeInventoryTableModel>();

            Task TaskGetAllListShoeInventory2 = Task.Run(delegate()
            {
                listShoeInventoryTableModel = (from shoeInventoryTableModel in shoeInventoryDBDataClassesDataContext.ShoeInventoryTables
                                               select new ShoeInventoryTableModel()
                                               {
                                                   id = Convert.ToInt32(shoeInventoryTableModel.Id),
                                                   shoeName = shoeInventoryTableModel.ShoeName.ToString(),
                                                   shoeDescription = shoeInventoryTableModel.ShoeDescription.ToString(),
                                                   shoePrice = shoeInventoryTableModel.ShoePrice.ToString(),
                                                   shoeSize = Convert.ToInt32(shoeInventoryTableModel.ShoeSize),
                                                   quantity = Convert.ToInt32(shoeInventoryTableModel.Quantity)
                                               }).ToList<ShoeInventoryTableModel>();
            });
            await TaskGetAllListShoeInventory2;

            if (listShoeInventoryTableModel.Count == 0)
            {
                return NotFound();
            }
            return Ok(listShoeInventoryTableModel);
        }


        [HttpGet]
        [Route("api/Application/getShoeInventoryItemId")]
        public async Task<IHttpActionResult> getShoeInventoryItemId(int id)
        {
            ShoeInventoryTableModel shoeInventoryTableModel = new ShoeInventoryTableModel();

            Task TaskGetShoeInventoryItemId = Task.Run(delegate()
            {
                shoeInventoryTableModel = (from shoeInventoryTable in shoeInventoryDBDataClassesDataContext.ShoeInventoryTables
                                           where shoeInventoryTable.Id == id
                                           select new ShoeInventoryTableModel()
                                           {
                                               id = Convert.ToInt32(shoeInventoryTable.Id),
                                               shoeName = shoeInventoryTable.ShoeName.ToString(),
                                               shoeDescription = shoeInventoryTable.ShoeDescription.ToString(),
                                               shoePrice = shoeInventoryTable.ShoePrice.ToString(),
                                               shoeSize = Convert.ToInt32(shoeInventoryTable.ShoeSize),
                                               quantity = Convert.ToInt32(shoeInventoryTable.Quantity)

                                           }).FirstOrDefault();
            });
            await TaskGetShoeInventoryItemId;

            if (shoeInventoryTableModel == null)
            {
                return NotFound();
            }
            return Ok(shoeInventoryTableModel);
        }


        public void MethodAddNewShoeInventoryItem(ShoeInventoryTableModel shoeInventoryTableModel)
        {
            App_Data.ShoeInventoryTable shoeInventoryTable = new App_Data.ShoeInventoryTable();

            shoeInventoryTable.Id = shoeInventoryTableModel.id;
            shoeInventoryTable.ShoeName = shoeInventoryTableModel.shoeName;
            shoeInventoryTable.ShoeDescription = shoeInventoryTableModel.shoeDescription;
            shoeInventoryTable.ShoePrice = shoeInventoryTableModel.shoePrice;
            shoeInventoryTable.ShoeSize = shoeInventoryTableModel.shoeSize;
            shoeInventoryTable.Quantity = shoeInventoryTableModel.quantity;

            shoeInventoryDBDataClassesDataContext.ShoeInventoryTables.InsertOnSubmit(shoeInventoryTable);
            shoeInventoryDBDataClassesDataContext.SubmitChanges();
        }

        [HttpPost]
        [Route("api/Application/addNewShoeInventoryItem")]
        public async Task<IHttpActionResult> addNewShoeInventoryItem([FromBody] ShoeInventoryTableModel shoeInventoryTableModel)
        {
            if (shoeInventoryTableModel == null)
            {
                return BadRequest("Invalid data.");
            }

            Task TaskaddNewShoeInventoryItem =
                Task.Run(() => MethodAddNewShoeInventoryItem(shoeInventoryTableModel));

            await TaskaddNewShoeInventoryItem;
            return Ok();
        }




        [HttpPost]
        [Route("api/Application/addNewShoeInventoryItem2")]
        public async  Task<IHttpActionResult> addNewShoeInventoryItem2([FromBody] PostShoeInventoryTableModel postShoeInventoryTableModel)
        {
            if (postShoeInventoryTableModel == null)
            {
                return BadRequest("Invalid data.");
            }

            Task TaskAddNewShoeInventoryItem2 = Task.Run(delegate()
            {
                App_Data.ShoeInventoryTable shoeInventoryTable = new App_Data.ShoeInventoryTable();

                shoeInventoryTable.ShoeName = postShoeInventoryTableModel.shoeName;
                shoeInventoryTable.ShoeDescription = postShoeInventoryTableModel.shoeDescription;
                shoeInventoryTable.ShoePrice = postShoeInventoryTableModel.shoePrice;
                shoeInventoryTable.ShoeSize = postShoeInventoryTableModel.shoeSize;
                shoeInventoryTable.Quantity = postShoeInventoryTableModel.quantity;

                shoeInventoryDBDataClassesDataContext.ShoeInventoryTables.InsertOnSubmit(shoeInventoryTable);
                shoeInventoryDBDataClassesDataContext.SubmitChanges();
            });

            await TaskAddNewShoeInventoryItem2;
            return Ok();
        }






        [HttpPut]
        [Route("api/Application/updateShoeInventoryItemDetail")]
        public async  Task<IHttpActionResult> updateShoeInventoryItemDetail([FromBody] ShoeInventoryTableModel shoeInventoryTableModel)
        {
            if (shoeInventoryTableModel == null)
            {
                return BadRequest("Invalid data.");
            }
            App_Data.ShoeInventoryTable shoeInventoryTable = new App_Data.ShoeInventoryTable();
            shoeInventoryTable = shoeInventoryDBDataClassesDataContext.ShoeInventoryTables.SingleOrDefault(u => u.Id == shoeInventoryTableModel.id);

            if (shoeInventoryTable != null)
            {
                Task TaskUpdateShoeInventoryItemDetail = Task.Run(delegate()
                {
                    shoeInventoryTable.Id = (int)shoeInventoryTableModel.id;
                    shoeInventoryTable.ShoeName = (string)shoeInventoryTableModel.shoeName;
                    shoeInventoryTable.ShoeDescription = (string)shoeInventoryTableModel.shoeDescription;
                    shoeInventoryTable.ShoePrice = (string)shoeInventoryTableModel.shoePrice;
                    shoeInventoryTable.ShoeSize = (int)shoeInventoryTableModel.shoeSize;
                    shoeInventoryTable.Quantity = (int)shoeInventoryTableModel.quantity;
                    shoeInventoryDBDataClassesDataContext.SubmitChanges();
                });
                await TaskUpdateShoeInventoryItemDetail;
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }



        [HttpDelete]
        [Route("api/Application/deleteShoeInventoryItemId")]
        public async Task<IHttpActionResult> deleteShoeInventoryItemId(int shoeId)
        {
            ShoeInventoryTableModel shoeInventoryTableModel = new ShoeInventoryTableModel();

            shoeInventoryTableModel = (from shoeInventoryTables in shoeInventoryDBDataClassesDataContext.ShoeInventoryTables
                                       where shoeInventoryTables.Id == shoeId
                                       select new ShoeInventoryTableModel()
                                       {
                                           id = Convert.ToInt32(shoeInventoryTables.Id),
                                           shoeName = shoeInventoryTables.ShoeName.ToString(),
                                           shoeDescription = shoeInventoryTables.ShoeDescription.ToString(),
                                           shoePrice = shoeInventoryTables.ShoePrice.ToString(),
                                           shoeSize = Convert.ToInt32(shoeInventoryTables.ShoeSize),
                                           quantity = Convert.ToInt32(shoeInventoryTables.Quantity)
                                       }).FirstOrDefault();

            if (shoeInventoryTableModel == null)
            {
                return NotFound();
            }

            Task TaskdeleteShoeInventoryItemId = Task.Run(delegate ()
            {
                App_Data.ShoeInventoryTable shoeInventoryTable = new App_Data.ShoeInventoryTable();
                shoeInventoryTable.Id = shoeInventoryTableModel.id;
                shoeInventoryTable.ShoeName = shoeInventoryTableModel.shoeName;
                shoeInventoryTable.ShoeDescription = shoeInventoryTableModel.shoeDescription;
                shoeInventoryTable.ShoePrice = shoeInventoryTableModel.shoePrice;
                shoeInventoryTable.ShoeSize = shoeInventoryTableModel.shoeSize;
                shoeInventoryTable.Quantity = shoeInventoryTableModel.quantity;
                shoeInventoryDBDataClassesDataContext.ShoeInventoryTables.Attach(shoeInventoryTable);
                shoeInventoryDBDataClassesDataContext.ShoeInventoryTables.DeleteOnSubmit(shoeInventoryTable);
                shoeInventoryDBDataClassesDataContext.SubmitChanges();
            });

            await TaskdeleteShoeInventoryItemId;
            return Ok();
        }






    }//end-class
}//end-namespace
