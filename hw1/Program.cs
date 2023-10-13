using hw1;

var grand_father = new AdultFamilyMember() { Mother = null, Father = null, Name = "Bob", Sex = Gender.Male };
var father = new AdultFamilyMember() { Mother = null, Father = grand_father, Name = "Jack", Sex = Gender.Male };
var mother = new AdultFamilyMember() { Mother = null, Father = null, Name = "Ann", Sex = Gender.Female};
var son1 = new FamilyMember() { Mother = mother, Father = father, Name = "Bill", Sex = Gender.Male };
var son2 = new FamilyMember() { Mother = mother, Father = father, Name = "Tom",  Sex = Gender.Male };
var son3 = new FamilyMember() { Mother = mother, Father = father, Name = "Alan", Sex = Gender.Male };
grand_father.Children = new FamilyMember[] { father };
father.Children = new FamilyMember[] {son1, son2, son3 };
mother.Children = new FamilyMember[] {son1, son2, son3 };
grand_father.Print(2);
Console.ReadLine();