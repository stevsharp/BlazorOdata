using BlazorOdata.Client.Model;
using BlazorOdata.Shared;
using Microsoft.AspNetCore.Components;
using Simple.OData.Client;

namespace BlazorOdata.Client.Pages
{
    public partial class Counter
    {

        private int currentCount = 0;

        [Inject]
        public IOdataManager DOdataManager
        {
            get;
            set;
        }

        private async Task IncrementCount()
        {
          

            try
            {

                var products = await DOdataManager.oDataClient
                    .For<Template>()
                    .FindEntriesAsync();

                var t = new Template()
                {
                    Id = 1,
                    Description = "fasdf",
                    IsActive = 1
                };

                var x = ODataDynamic.Expression;
                //var product = await DOdataManager.oDataClient
                //    .For(x.Template)
                //    .Set(x.Description = "Test1", x.IsActive = 1)
                //    .InsertEntryAsync();

                var product = await DOdataManager.oDataClient
                    .For<Template>()
                    .Set(new
                    {
                        t.Description,
                        t.IsActive
                    })
                    .InsertEntryAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }


        }
    }
}

