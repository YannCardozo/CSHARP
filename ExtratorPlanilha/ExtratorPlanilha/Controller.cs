using Google.Apis.Sheets.v4;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtratorPlanilha
{
    public class Controller
    {
        [Route("api/[controller]")]
        [ApiController]
        public class ItemsController : ControllerBase
        {
            const string SPREADSHEET_ID = "10abkaYT-eFq98705nLOxj3g1-0ywlGx-0UuGkioDKkw";
            const string SHEET_NAME = "Página1";
            SpreadsheetsResource.ValuesResource _googleSheetValues;
            public ItemsController(GoogleSheetsHelper googleSheetsHelper)
            {
                _googleSheetValues = googleSheetsHelper.Service.Spreadsheets.Values;
            }
            [HttpGet]
            public IActionResult Get()
            {
                var range = $"{SHEET_NAME}!A:E";
                var request = _googleSheetValues.Get(SPREADSHEET_ID, range);
                var response = request.Execute();
                var values = response.Values;
                return Ok(ItemsMapper.MapFromRangeData(values));
            }
            [HttpGet("{rowId}")]
            public IActionResult Get(int rowId)
            {
                var range = $"{SHEET_NAME}!A{rowId}:E{rowId}";
                var request = _googleSheetValues.Get(SPREADSHEET_ID, range);
                var response = request.Execute();
                var values = response.Values;
                return Ok(ItemsMapper.MapFromRangeData(values).FirstOrDefault());
            }


        }
    }
}
