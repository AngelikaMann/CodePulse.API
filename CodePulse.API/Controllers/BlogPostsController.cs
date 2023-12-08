﻿using CodePulse.API.Models.Domain;
using CodePulse.API.Models.DTO;
using CodePulse.API.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CodePulse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostsController : ControllerBase
    {
        private readonly IBlogPostRepository blogPostRepository;

        public BlogPostsController(IBlogPostRepository blogPostRepository)
        {
            this.blogPostRepository = blogPostRepository;
        }

        //Post:{apibaseurl}/api/blogposts
        [HttpPost]
        public async Task<IActionResult> CreateBlogPosts([FromBody] CreateBlogPostRequestDto request)
        {
            // Convert DTO to Domain Model
            var blogPost = new BlogPost
            {

                Author = request.Author,
                Content = request.Content,
                FeaturedImageUrl = request.FeaturedImageUrl,
                IsVisible = request.IsVisible,
                PublishedDate = request.PublishedDate,
                ShortDescription = request.ShortDescription,
                Title = request.Title,
                UrlHandle = request.UrlHandle
            };
            blogPost = await blogPostRepository.CreateAsync(blogPost);

            //convert Domain Model to  DTO
            var response = new BlogPostDto
            {
                Id = blogPost.Id,
                Author = blogPost.Author,
                Content = blogPost.Content,
                FeaturedImageUrl = blogPost.FeaturedImageUrl,
                PublishedDate = blogPost.PublishedDate,
                IsVisible = blogPost.IsVisible,
                ShortDescription = blogPost.ShortDescription,
                Title = blogPost.Title,
                UrlHandle = blogPost.UrlHandle,

            };
            return Ok(response);

        }


        //Get:{apibaseurl}/api/blogposts
        [HttpGet]

        public async Task<IActionResult> getAllBlogPosts()
        {
            var blogPosts = await blogPostRepository.GetAllAsync();
            //Convert Domain model to DTO

            var response = new List<BlogPostDto>();

            foreach (var blogPost in blogPosts)
            {
                response.Add(new BlogPostDto
                {
                    Id = blogPost.Id,
                    Author = blogPost.Author,
                    Content = blogPost.Content,
                    FeaturedImageUrl = blogPost.FeaturedImageUrl,
                    PublishedDate = blogPost.PublishedDate,
                    IsVisible = blogPost.IsVisible,
                    ShortDescription = blogPost.ShortDescription,
                    Title = blogPost.Title,
                    UrlHandle = blogPost.UrlHandle,
                });
            }
            return Ok(response);
        }

    }
}