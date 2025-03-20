using System;
namespace EntityLayer.Entities
{
    public class Exam
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Question> Questions { get; set; }
    }
}