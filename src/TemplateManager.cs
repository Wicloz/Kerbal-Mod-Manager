using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace KSP_Mod_Manager
{
    [Serializable]
    public class TemplateInfo
    {
        public string name;
        public string category;
        public string website;
        public string dlSite;
        public string originalZip;

        public TemplateInfo()
        { }

        public TemplateInfo(string Name, string Category, string Website, string DlSite)
        {
            this.name = Name;
            this.category = Category;
            this.website = Website;
            this.dlSite = DlSite;
            this.originalZip = "none";
        }
    }

    public class TemplateManager
    {
        private string templateFilePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\KMM\\templates.dat";
        public List<TemplateInfo> templates = new List<TemplateInfo>();

        public void LoadTemplates()
        {
            if (File.Exists(templateFilePath))
            {
                templates = SaveLoad.LoadFileBf<List<TemplateInfo>>(templateFilePath);
            }
            else
            {
                templates = new List<TemplateInfo>();
            }

            Main.acces.LogMessage("Templates Loaded");
        }

        public void SaveTemplates()
        {
            if (templates == null)
            {
                MessageBox.Show("Templates File corrupted, deleting ...", "ERROR");
                File.Delete(templateFilePath);

                LoadTemplates();
            }

            foreach (ModInfo mod in Main.acces.modInfo.modList)
            {
                if (!mod.zipfile.Contains("Overrides\\"))
                {
                    TemplateInfo sendplate = null;

                    foreach (TemplateInfo template in templates)
                    {
                        if (MiscFunctions.CleanName(mod.name).Replace("v", "") == MiscFunctions.CleanName(template.name).Replace("v", "") || MiscFunctions.CleanName(mod.zipfile) == MiscFunctions.CleanName(template.originalZip))
                        {
                            sendplate = template;
                            break;
                        }
                    }

                    if (sendplate != null)
                    {
                        sendplate.name = mod.name;
                        sendplate.category = mod.category;
                        sendplate.website = mod.website;
                        sendplate.dlSite = mod.dlSite;

                        if (mod.hasZipfile && MiscFunctions.CleanName(mod.zipfile).Replace("v", "") != MiscFunctions.CleanName(mod.name).Replace("v", ""))
                        {
                            sendplate.originalZip = mod.zipfile;
                        }
                    }
                    else if (mod.hasZipfile)
                    {
                        sendplate = new TemplateInfo(mod.name, mod.category, mod.website, mod.dlSite);
                        sendplate.originalZip = mod.zipfile;

                        templates.Add(sendplate);
                    }
                }
            }

            SaveLoad.SaveFileBf(templates, templateFilePath);
            Main.acces.LogMessage("Templates Saved");
        }
    }
}
