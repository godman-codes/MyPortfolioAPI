using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using Shared.DTOs;
using Shared.DTOs.Response;
using System.Text;

namespace MyPortfolioAPI
{
    public class CsvOutputFormatter : TextOutputFormatter
    {
        public CsvOutputFormatter()
        {
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/csv"));
            SupportedEncodings.Add(Encoding.UTF8);
            SupportedEncodings.Add(Encoding.Unicode);
        }

        protected override bool CanWriteType(Type? type)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));

            // Check if the type is any of the supported DTO types
            if (typeof(TechnologyResponseDto).IsAssignableFrom(type) ||
                typeof(TokenDto).IsAssignableFrom(type) ||
                typeof(ProjectResponseDto).IsAssignableFrom(type) ||
                typeof(WorkExperienceResponseDto).IsAssignableFrom(type) ||
                typeof(IEnumerable<TechnologyResponseDto>).IsAssignableFrom(type) ||
                typeof(IEnumerable<ProjectResponseDto>).IsAssignableFrom(type) ||
                typeof(IEnumerable<WorkExperienceResponseDto>).IsAssignableFrom(type))
            {
                return base.CanWriteType(type);
            }

            return false;
        }

        public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
        {
            var response = context.HttpContext.Response;
            var buffer = new StringBuilder();

            if (context.Object is IEnumerable<TechnologyResponseDto>)
            {
                foreach (var tech in (IEnumerable<TechnologyResponseDto>)context.Object)
                {
                    FormatCsv(buffer, tech);
                }
            }
            else if (context.Object is IEnumerable<ProjectResponseDto>)
            {
                foreach (var project in (IEnumerable<ProjectResponseDto>)context.Object)
                {
                    FormatCsv(buffer, project);
                }
            }
            else if (context.Object is IEnumerable<WorkExperienceResponseDto>)
            {
                foreach (var workExperience in (IEnumerable<WorkExperienceResponseDto>)context.Object)
                {
                    FormatCsv(buffer, workExperience);
                }
            }
            else if (context.Object is TechnologyResponseDto)
            {
                FormatCsv(buffer, (TechnologyResponseDto)context.Object);
            }
            else if (context.Object is ProjectDto)
            {
                FormatCsv(buffer, (ProjectResponseDto)context.Object);
            }
            else if (context.Object is WorkExperienceDto)
            {
                FormatCsv(buffer, (WorkExperienceResponseDto)context.Object);
            }

            await response.WriteAsync(buffer.ToString());
        }

        private static void FormatCsv(StringBuilder buffer, TechnologyResponseDto technology)
        {
            buffer.AppendLine($"{technology.Id},\"{technology.Name},\"{technology.Percentage}\"");
        }

        private static void FormatCsv(StringBuilder buffer, ProjectResponseDto project)
        {
            buffer.AppendLine($"{project.Id},\"{project.Name},\"{project.Description}\",\"{project.GitHubLink}\",\"{project.ProductionLink}\",\"{project.Role}\",\"{project.Tools}\"");
        }

        private static void FormatCsv(StringBuilder buffer, WorkExperienceResponseDto workExperience)
        {
            buffer.AppendLine($"{workExperience.Id},\"{workExperience.Company},\"{workExperience.Role}\",\"{workExperience.Description}\",\"{workExperience.From}\",\"{workExperience.To}\"");
        }
    }

}
