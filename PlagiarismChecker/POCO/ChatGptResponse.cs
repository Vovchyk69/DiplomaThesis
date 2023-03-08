using System.Collections.Generic;
using System.Linq;

namespace PlagiarismChecker.POCO
{
    public record ChatGptResponse(IEnumerable<string> Choices = null, ChatGptResponseError Error = null)
    {
        public IEnumerable<string> Choices = Choices ?? Enumerable.Empty<string>();
    }
}
