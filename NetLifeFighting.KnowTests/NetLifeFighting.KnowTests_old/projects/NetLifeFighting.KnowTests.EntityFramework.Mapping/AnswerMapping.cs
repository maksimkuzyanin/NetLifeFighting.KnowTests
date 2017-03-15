using System.Data.Entity.ModelConfiguration;
using NetLifeFighting.KnowTests.Common.ObjectModel.EntityFramework;

namespace NetLifeFighting.KnowTests.EntityFramework.Mapping
{
	public class AnswerMapping : EntityTypeConfiguration<Answer>
	{
		public AnswerMapping()
		{
			ToTable("Answer");

			HasKey(x => x.AnswerId);

			Property(x => x.Title).IsRequired().HasMaxLength(200);
			Property(x => x.Literal).HasMaxLength(10);
			Property(x => x.IsRight).IsRequired();
			Property(x => x.PriorityNo);

			HasRequired(x => x.Question).WithRequiredPrincipal().Map(x => x.MapKey("QuestId"));
		}
	}
}
