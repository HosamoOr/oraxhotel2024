using DataModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotelSys.ViewModel
{

	public partial class TreeAccountViewModel
	{
		 public String Id { get; set; } 
	
		public string Name { get; set; }

        public string showName { get; set; }
        // public int? IdMainGroup { get; set; }
        public bool? IsRoot { get; set; }
		 public bool? IsPrivate { get; set; }
		 public int? IdSub { get; set; }

        public String mainORsub { get; set; } // رئيسي(مجموعة) فرعي(حساب)ب

		//for account
		//public List<TreeAccountViewModel> Items { get; set; }

		public string Status { get; set; }
		public DateTime? Createat { get; set; }
		public String? IdGroup { get; set; }

		public int? Code { get; set; }
		

		//for view  js
	    public bool	expanded { get; set; }
		public String icon { get; set; }

        //for view rasor
        public string Icon { get; set; }
        public bool IsDirectory { get; set; }
        public bool IsExpanded { get; set; }


    }

    //public class TreeAccountViewModel
    //{
    //    public string Id { get; set; }
    //    public string IdGroup { get; set; }
    //    public string Name { get; set; }
    //    public string Icon { get; set; }
    //    public bool IsDirectory { get; set; }
    //    public bool IsExpanded { get; set; }
    //    public IEnumerable<TreeAccountViewModel> Items { get; set; }
    //}


    //public static class TreeViewPlainDataForDragAndDrop
    //{
    //        public static readonly IEnumerable<TreeAccountViewModel> FileSystemItems = new[] {
    //            new TreeAccountViewModel {
    //                Id = "1",
    //                Name =  "Documents",
    //                Icon = "activefolder",
    //                IsDirectory= true,
    //                IsExpanded = true
    //            }, new TreeAccountViewModel {
    //                Id = "2",
    //                IdGroup = "1",
                
    //                Name =  "Projects",
    //                Icon = "activefolder",
    //                IsDirectory= true,
    //                IsExpanded = true
    //            }, new TreeAccountViewModel {
    //                Id = "3",
    //                IdGroup = "2",
    //                Name =  "About.rtf",
    //                Icon = "file",
    //                IsDirectory= false
    //            }, new TreeAccountViewModel {
    //                Id = "4",
    //                IdGroup = "2",
    //                Name =  "Passwords.rtf",
    //                Icon = "file",
    //                IsDirectory= false
    //            }, new TreeAccountViewModel {
    //                Id = "5",
    //                IdGroup = "2",
    //                Name =  "About.xml",
    //                Icon = "file",
    //                IsDirectory= false
    //            }, new TreeAccountViewModel {
    //                Id = "6",
    //                IdGroup = "2",
    //                Name =  "Managers.rtf",
    //                Icon = "file",
    //                IsDirectory= false
    //            }, new TreeAccountViewModel {
    //                Id = "7",
    //                IdGroup = "2",
    //                Name =  "ToDo.txt",
    //                Icon = "file",
    //                IsDirectory= false
    //            }, new TreeAccountViewModel {
    //                Id = "8",
    //                Name =  "Images",
    //                Icon = "activefolder",
    //                IsDirectory= true,
    //                IsExpanded = true
    //            }, new TreeAccountViewModel {
    //                Id = "9",
    //                IdGroup = "8",
    //                Name =  "logo.png",
    //                Icon = "file",
    //                IsDirectory= false
    //            }, new TreeAccountViewModel {
    //                Id = "10",
    //                IdGroup = "8",
    //                Name =  "banner.gif",
    //                Icon = "file",
    //                IsDirectory= false
    //            }, new TreeAccountViewModel {
    //                Id = "11",
    //                Name =  "System",
    //                Icon = "activefolder",
    //                IsDirectory= true,
    //                IsExpanded = true
    //            }, new TreeAccountViewModel {
    //                Id = "12",
    //                IdGroup = "11",
    //                Name =  "Employees.txt",
    //                Icon = "file",
    //                IsDirectory= false
    //            }, new TreeAccountViewModel {
    //                Id = "13",
    //                IdGroup = "11",
    //                Name =  "PasswordList.txt",
    //                Icon = "file",
    //                IsDirectory= false,
    //            }, new TreeAccountViewModel {
    //                Id = "14",
    //                Name =  "Description.rtf",
    //                Icon = "file",
    //                IsDirectory= false
    //            }, new TreeAccountViewModel {
    //                Id = "15",
    //                Name =  "Description.txt",
    //                Icon = "file",
    //                IsDirectory= false
    //            }
    //        };
    
    
    //}



}
