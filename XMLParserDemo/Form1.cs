using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace XMLParserDemo
{
    public partial class Form1 : Form
    {
        private int counter = 0;
        private int maxCount = 100;
        private List<string> names =
            new List<string>() {
                "Microsoft",
                "Apple",
                "Oracle",
                "IBM",
                "Яндекс",
                "ТОВ \"ТЕХМЕТМАШ\"",
                "Без имени"
            };
        BindingList<string> intersectionData = new BindingList<string>();

        public Form1()
        {
            InitializeComponent();
            intersectionsListBox.DataSource = intersectionData;
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = dialog.FileName;
                //Console.WriteLine(filePath);
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(filePath);
                //printNodes(xmlDocument);
                /*XmlNode dataNode =
                    ((XmlElement)xmlDocument.GetElementsByTagName("DATA")[0].ChildNodes[0]).GetElementsByTagName("NAME")[0];*/
                //Console.WriteLine(dataNode.InnerText);
                //this.Text = dataNode.InnerText;

                XmlNodeList recordNodes =
                    xmlDocument.GetElementsByTagName("DATA")[0].ChildNodes;

                var intersectionsList =
                    recordNodes.Cast<XmlNode>()
                        .Select(record => ((XmlElement)record).GetElementsByTagName("NAME")[0].InnerText)
                        .Intersect(names);

                foreach (var item in intersectionsList)
                {
                    intersectionData.Add(item);
                }

                foreach (var item in intersectionData)
                {
                    Console.WriteLine(item);
                }

                
            }
        }

        private void printNodes(XmlNode node) {
            foreach (XmlNode item in node.ChildNodes)
            {
                if (counter < maxCount)
                {
                    Console.WriteLine(item.Name + " = " + item.Value);
                    Console.Write("\t");
                    if (item.HasChildNodes)
                    {
                        printNodes(item);
                    }
                    counter++;
                }
            }
        }
    }
}
