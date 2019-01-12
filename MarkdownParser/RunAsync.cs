using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MarkdownParser
{
    public class RunAsync
    {
        //Test App
        public async Task Run()
        {
            var rawData = await GetRawData("https://raw.githubusercontent.com/discord-bot-tutorial/common-issues/master/README.md");
            var splitData = Split(rawData);

            var headings2 = ParseSubHeadingTwo(splitData);
            var headings3 = ParseSubHeadingThree(splitData);

            Console.WriteLine(ParseHeading(splitData));

            headings2.ForEach(x =>
            {
                if (x.Contains("###"))
                    Console.WriteLine(x.Replace("### ", ""));
            });

            headings3.ForEach(x =>
            {
                Console.WriteLine(x);
            });

            Console.ReadLine();
        }

        //Parse Heading (#)
        private string ParseHeading(string[] source)
        {
            var s = source.Where(x => x.StartsWith("#")).ToList();
            return s.Where(x => x.StartsWith("#")).FirstOrDefault().Replace("# ", "");
        }

        //Parse Heading 2 (##)
        private List<string> ParseSubHeadingTwo(string[] source)
        {
            var r = new List<string>();
            var s = source.Where(x => x.StartsWith("##") && !x.StartsWith("###")).ToList();
            s.ForEach(x =>
            {
                r.Add(x.Replace("## ", ""));
            });
            return r;
        }

        //Parse Heading 3 (###)
        private List<string> ParseSubHeadingThree(string[] source)
        {
            var r = new List<string>();
            var s = source.Where(x => x.StartsWith("###") && !x.StartsWith("####")).ToList();
            s.ForEach(x =>
            {
                r.Add(x.Replace("### ", ""));
            });
            return r;
        }

        //parse Links
        private List<string> ParseLinks(string[] source)
        {

        }

        //Split our source data by newlines.
        private string[] Split(string source)
        {
            return source.Split(
                new[] { "\r\n", "\r", "\n" },
                StringSplitOptions.None);
        }

        //Get the Raw Markdown
        private async Task<string> GetRawData(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage message = await client.GetAsync(url);
                    message.EnsureSuccessStatusCode();
                    return await message.Content.ReadAsStringAsync();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex);
                }
            }
        }


    }
}
