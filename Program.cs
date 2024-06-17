using System;
using System.Data;
using Newtonsoft.Json;  
using Newtonsoft.Json.Linq;
using apmssql;


var  MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy  =>
                      {
                          policy.WithOrigins("http://127.0.0.1:5500",
                                              "http://127.0.0.1",
                                              "http://localhost:5500");
                      });
});

var app = builder.Build();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseCors(MyAllowSpecificOrigins);

app.MapGet("/", () => "Hello World!");
app.MapGet("/test", () => {
    DataTable dt;
    string s1=string.Empty;           
    MssqlR t1=new MssqlR();  
    string vsql;    
    //這段是原始查詢結果,要加工查詢,net不給過,全段可能要重理,20240617改用left join
    vsql="select top 100 CONTENT.NEWS_TITLE,CONTENT_SUBITEM.S_NEWS_TITLE,";
    vsql=vsql+"CONTENT_SUBITEM.S_NEW_Title,CONTENT_SUBITEM.S_NEW_Special,";
    vsql=vsql+"CONTENT_SUBITEM.S_NEW_Promotions,CONTENT_SUBITEM.S_NEW_Attach,CONTENT_SUBITEM.S_HORN,";
    vsql=vsql+"CONTENT_SUBITEM.S_DAY,CONTENT_SSUBITEM3.SS_NNO,";
    vsql=vsql+"CONTENT_SSUBITEM3.SS_TO,CONTENT_SSUBITEM3.SS_NEWS_TITLE,";
    vsql=vsql+"CONTENT_SSUBITEM3.SS_SUB_TITLE,CONTENT_SSUBITEM3.SS_SUB_TITLE1,";
    vsql=vsql+"CONTENT_SSUBITEM3.SS_SUB_TITLE2,";
    vsql=vsql+"CONTENT_SSUBITEM3.SS_Reminder,CONTENT_SSUBITEM3.SS_VORN,CONTENT_SSUBITEM3.SS_TORN,";
    vsql=vsql+"CONTENT_SSUBITEM3.SS_FORN,CONTENT_SSUBITEM3.SS_APPLY,CONTENT_SSUBITEM3.SS_PORN,";
    vsql=vsql+"CONTENT_SSUBITEM3.SS_TTYPE,CONTENT_SSUBITEM3.SS_ADATE";
    vsql=vsql+"from CONTENT,CONTENT_SUBITEM,CONTENT_SSUBITEM3";
    vsql=vsql+"";  
    dt=t1.RunSQL("select NEWS_CONTENT,NEWS_DISPLAY from EDM_CONTENT where NNO=4 or NNO=5 order by NNO ASC");                			
   
    s1=JsonConvert.SerializeObject(dt);                                                        
    return s1;
});

app.Run();