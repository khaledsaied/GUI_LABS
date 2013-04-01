using System;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace Part_4
{
   public class Agents : ObservableCollection<Agent>
   {
       public Agents()
       {
           Add(new Agent("007", "Bo", "adresse", "spec", "opgav"));
           Add(new Agent("911", "KS", "nyadresse", "speciale", "opgaverre"));
       }
   }  

   [Serializable]
   public class Agent : INotifyPropertyChanged
   {

      string id;
      string codeName;
      string speciality;
      string assignment;

      public Agent()
      {
      }

      public event PropertyChangedEventHandler PropertyChanged;
      protected void Notify(string propName)
      {
          if (this.PropertyChanged != null)
          {
              PropertyChanged(this, new PropertyChangedEventArgs(propName));
          }
      }

      public Agent(string aId, string aName, string aAddress, string aSpeciality, string aAssignment)
      {
         id = aId;
         codeName = aName;
         speciality = aSpeciality;
         assignment = aAssignment;
      }

      public string ID
      {
         get
         {
            return id;
         }
         set
         {
            id = value;
            Notify("ID");
         }
      }

      public string CodeName
      {
         get
         {
            return codeName;
         }
         set
         {
            codeName = value;
            Notify("CodeName");
         }
      }

      public string Speciality
      {
         get
         {
            return speciality;
         }
         set
         {
            speciality = value;
            Notify("Speciality");
         }
      }

      public string Assignment
      {
         get
         {
            return assignment;
         }
         set
         {
            assignment = value;
            Notify("Assignment");
         }
      }
   }
}
