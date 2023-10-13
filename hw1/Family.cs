using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw1
{
    public enum Gender
    {
        Male,
        Female
    }

    public class FamilyMember
    {
        public FamilyMember Mother { get; set; }
        public FamilyMember Father { get; set; }
        public string Name { get; set; }
        public Gender Sex { get; set; }

        public void Info(int indent = 0, string info = "")
        {
            Console.WriteLine($"{new String('-', indent)}Имя {this.Name} ({info})");
        }
        public virtual void Print(int indent = 0, string info = "")
        {
            Info(indent, info);
        }


    }

    public class AdultFamilyMember : FamilyMember
    {
        public FamilyMember[] Children { get; set; }
        public override void Print(int indent = 0, string info = "")
        {
            base.Print(indent);
            if (Children != null && Children.Length != 0)
            {
                bool searchWifeOrHusband = true;
                foreach (var child in Children)
                {
                    if (searchWifeOrHusband)
                    {
                        if (child.Father != null && child.Father.Equals(this) && child.Mother != null) 
                        {
                            (child.Mother as AdultFamilyMember)?.PrintWifeOrHusband(indent);
                        }
                        else if(child.Mother != null && child.Mother.Equals(this) && child.Father != null)
                        {
                            (child.Father as AdultFamilyMember)?.PrintWifeOrHusband(indent);
                        }
                        searchWifeOrHusband = false;
                    }
                    child.Print(indent * 2);
                }
            }
        }
        void PrintWifeOrHusband(int indent)
        {
            string info;
            if (this.Sex == Gender.Female)
                info = "жена";
            else
                info = "муж";
            base.Print(indent, info);
        }
    }
}
