using OpenQA.Selenium;
using System.Collections.Generic;
using Sitegeist.Utils;

namespace Sitegeist
{
    public class PageCataloger
    {
        internal IJavaScriptExecutor js { get; set; }
        internal IWebDriver webDriver { get; private set; }
        
        public PageCataloger(IWebDriver WebDriver)
        {
            webDriver = WebDriver;            
        }

        void setScript(IWebDriver webDriver)
        {           
            js = (IJavaScriptExecutor)webDriver;

            var pathTo = @"function getPathTo(element)
{
    if (element.tagName == 'HTML')
        return '/HTML[1]';
    if (element === document.body)
        return '/HTML[1]/BODY[1]';

    var ix = 0;
    var siblings = element.parentNode.childNodes;
    for (var i = 0; i < siblings.length; i++)
    {
        var sibling = siblings[i];
        if (sibling === element)
            return getPathTo(element.parentNode) + '/' + element.tagName + '[' + (ix + 1) + ']';
        if (sibling.nodeType === 1 && sibling.tagName === element.tagName)
            ix++;
    }
}";

            var document = @"
            var txt = ''; 
            var active = false; 
            
            var btn = document.createElement('button');
            btn.style ='position:absolute;top:1px;left:1px;width:100px;height:30px';
            btn.innerHTML = 'Enable';
            btn.setAttribute('id', 'btnID');
            btn.addEventListener('click', function() { active = !active; if (active) { btn.innerHTML = 'Disable'} else { btn.innerHTML = 'Enable'} });
            document.body.appendChild(btn);

            function copy(text) {
                var input = document.createElement('input');
                input.setAttribute('value', text);
                document.body.appendChild(input);
                input.select();
                alert(document.getSelection());
                var result = document.execCommand('copy');
                document.body.removeChild(input);
             }

            document.addEventListener('contextmenu', function(e) {
                e.preventDefault();
                if (active) { 
                    msg = 'text:' + txt + '\r\n';                    
                    if (e.target.id) { 
                        msg += 'id:' + e.target.id + '\r\n';
                    }
                    if (e.target.name) { 
                        'name:' + e.target.name + '\r\n'; 
                    } 
                    msg += 'xpath:' + getPathTo(e.target); 
                    copy(msg); 
                    alert(msg);
                }
            }); 
            document.addEventListener('keydown', function(e) { 
                if (e.code.indexOf('Key') == 0) txt += e.code.substring(3);
            });";

            pathTo = pathTo.Replace("\r", "").Replace("\n", "").Replace("    ", " ").Replace("   ", " ").Replace("  ", " ");
            var fn = @"var s = document.createElement('script'); 
            s.textContent = """ + pathTo + @"""; 
            document.body.appendChild(s); " + document;
            js.ExecuteScript(fn);
        }

        public PageCatalog GenerateCatalog(string Url)
        {            
            webDriver.Url = Url;
            setScript(webDriver);

            var elements = GetElemntsByType("input");

            // var list2 = GetElemntsByType("H3");
            // elements.AddRange(GetElemntsByType("input"));
            // elements.AddRange(GetElemntsByType("a"));
            // elements.AddRange(GetElemntsByType("button"));

            // get DataTable
            // get Accordion
            // get Switchery
            // get Dialogs

            return new PageCatalog()
            {
                Url = Url,
                PageElements = elements
            };
        }

        internal List<PageElement> GetElemntsByType(string ElementType)
        {            
            var results = new List<PageElement>();
            var elements = webDriver.FindElements(By.TagName(ElementType));
            int i = 0;
            foreach (var element in elements)
            {
                i++;
                var style = $"z-index:99999;height:30px;width:30px;border:solid 1px black;position:absolute;top:{element.Location.Y}px;left:{element.Location.X}px";
                var script = "var div = document.createElement('DIV'); div.setAttribute('id', 'tmp_" + i + "'); " +
                    " div.style ='" + style + "'; document.body.appendChild(div); ";
                // js.ExecuteScript(script, null);

                var pe = new PageElement()
                {
                    ElementTag = ElementType,
                    ElementXPath = SeleniumXPathGenerator.GenerateXPath(element, ""),
                    HTML_id = element.GetAttribute("id"),
                    HTML_name = element.GetAttribute("name"),
                    ClassList = element.GetAttribute("class")
                };
                if (ElementType == "a")
                {
                    pe.OtherInfo = element.GetAttribute("href");
                }
                else
                {
                    pe.OtherInfo = element.GetAttribute("type");
                }

                results.Add(pe);
            }

            
            return results;
        }
    }

    public class PageCatalog
    {
        public string Url { get; set; }
        public List<PageElement> PageElements { get; set; }
    }

    public class PageElement
    {
        public string ClassList { get; set; }
        public string OtherInfo { get; set; }
        public string HTML_id { get; set; }
        public string HTML_name { get; set; }
        public string ElementXPath { get; set; }
        public string ElementTag { get; set; }
    }
}
