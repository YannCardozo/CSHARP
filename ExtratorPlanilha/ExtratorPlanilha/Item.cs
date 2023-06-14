using Google.Apis.Sheets.v4.Data;
using Google.Apis.Sheets.v4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static Google.Apis.Sheets.v4.SpreadsheetsResource.ValuesResource;
using Microsoft.AspNetCore.Mvc;

namespace ExtratorPlanilha
{
    public class Item
    {
        public string UF { get; set; }
        public string Cidade { get; set; }
        public string Bairro { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }

    public static class ItemsMapper
    {
        public static List<Item> MapFromRangeData(IList<IList<object>> values)
        {
            var items = new List<Item>();

            foreach (var value in values)
            {
                Item item = new()
                {
                    UF = value[0].ToString(),
                    Cidade = value[1].ToString(),
                    Bairro = value[2].ToString(),
                    Latitude = value[3].ToString(),
                    Longitude = value[3].ToString()
                };

                items.Add(item);
            }

            return items;
        }

        public static IList<IList<object>> MapToRangeData(Item item)
        {
            var objectList = new List<object>() { item.UF, item.Cidade, item.Bairro, item.Latitude, item.Longitude };
            var rangeData = new List<IList<object>> { objectList };
            return rangeData;
        }
    }
}

