using System;
using System.Collections.Generic;
using StudyAssignmentManager.Domain;
using Xunit;

namespace StudyAssignmentManager.Tests
{
    public class TestEducationalMaterialRepository
    {
        private static TestHelper GetTestHelper()
        {
            return new TestHelper();
        }

        private static EducationalMaterial CreateEducationalMaterial()
        {
            return new EducationalMaterial
            {
                AuthorId = new Guid("c0ca8eb7-53bc-49d9-ad87-9cc619aa8ea7"),
                Title = "test title",
                Description = "test description",
                Content = new EditorJSData
                {
                    Blocks = new List<EditorJSBlock>
                    {
                        new ()
                        {
                            Type = "header",
                            Data = new
                            {
                                Text = "test header",
                                Level = 2,
                            }
                        },
                        new ()
                        {
                            Type = "paragraph",
                            Data = new
                            {
                                Text = "test text",
                            }
                        },
                    }
                }
            };
        }

        [Fact]
        public void AddAsync()
        {
            var educationalMaterialRepository = GetTestHelper().EducationalMaterialRepository;

            var educationalMaterial = CreateEducationalMaterial();
            
            educationalMaterialRepository.AddAsync(educationalMaterial).Wait();
            var result = educationalMaterialRepository.GetByIdAsync(educationalMaterial.Id).Result;

            Assert.Equal(educationalMaterial.Id, result.Id);
            Assert.Equal(2, result.Content.Blocks.Count);
        }
        
        [Fact]
        public void UpdateAsync()
        {
            var educationalMaterialRepository = GetTestHelper().EducationalMaterialRepository;

            var educationalMaterial = CreateEducationalMaterial();
            educationalMaterialRepository.AddAsync(educationalMaterial).Wait();
            
            var educationalMaterialFromDb = educationalMaterialRepository.GetByIdAsync(educationalMaterial.Id).Result;
            educationalMaterialFromDb.Title = "test";
            educationalMaterialFromDb.Description = "test";
            educationalMaterialRepository.UpdateAsync(educationalMaterialFromDb);
            
            var result = educationalMaterialRepository.GetByIdAsync(educationalMaterial.Id).Result;
            Assert.Equal("test", result.Title);
            Assert.Equal("test", result.Description);
        }
        
        [Fact]
        public void DeleteAsync()
        {
            var educationalMaterialRepository = GetTestHelper().EducationalMaterialRepository;

            var educationalMaterial = CreateEducationalMaterial();
            educationalMaterialRepository.AddAsync(educationalMaterial).Wait();
            
            educationalMaterialRepository.DeleteAsync(educationalMaterial.Id);
            
            var result = educationalMaterialRepository.GetByIdAsync(educationalMaterial.Id).Result;
            Assert.True(result is null);
        }
        
        [Fact]
        public void GetByAuthorIdAsync()
        {
            var educationalMaterialRepository = GetTestHelper().EducationalMaterialRepository;

            var educationalMaterial1 = CreateEducationalMaterial();
            var educationalMaterial2 = CreateEducationalMaterial();
            var authorId = educationalMaterial1.AuthorId;
            
            educationalMaterialRepository.AddAsync(educationalMaterial1).Wait();
            educationalMaterialRepository.AddAsync(educationalMaterial2).Wait();
            var result = educationalMaterialRepository.GetByAuthorIdAsync(authorId).Result;

            Assert.Equal(2, result.Count);
        }
    }
}