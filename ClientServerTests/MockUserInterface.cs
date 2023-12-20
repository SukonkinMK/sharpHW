using DBLesson.Abstracts;

namespace ClientServerTests
{
    public class MockUserInterface : IUserInterface
    {
        private int calls = 0;
        public string ReadLine()
        {
            calls++;
            if (calls == 1)
                return "Sasha";
            else if (calls == 2)
                return "Hello";
            else
                return String.Empty;
        }

        public void Write(string str)
        {
            //throw new NotImplementedException();
        }

        public void WriteLine(string str)
        {
            //throw new NotImplementedException();
        }
    }
}