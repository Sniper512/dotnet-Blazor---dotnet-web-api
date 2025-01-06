//using Web_Api.Models;
//using static System.Net.WebRequestMethods;


//public class PostRepository
//    {
//        static HttpClient client = new HttpClient();

//        public string AddPost(Post post)
//        {
            

//            return "Post with this Id already exists!";
//        }

//        public string RemovePost(string Id)
//        {
//            int isDeleted = posts.RemoveAll(x => x.Id == Id);
//            if (isDeleted > 0)
//            {
//                return "Post deleted successfully!";
//            }
//            else
//            {
//                return "Post with this Id not found!";
//            }
//        }

//    static async Task<Post> GetProductAsync(string path)
//    {
//            Todos = await Http.Get;

//        return product;
//    }

//    public string UpdatePost(string Id, Post newPost)
//        {
//            var postIndex = posts.FindIndex(x => x.Id == Id);

//            if (postIndex >= 0) 
//            {
//                posts[postIndex] = newPost;
//                return "Post updated successfully!";
//            }

//            return "Post with this Id not found!";
//        }

//        public static List<Post> GenerateRandomPosts(int count)
//        {
//            var random = new Random();
//            var posts = new List<Post>();

//            for (int i = 1; i <= count; i++)
//            {
//                posts.Add(new Post
//                {
//                    Id = Guid.NewGuid().ToString(),
//                    Title = $"Post Title {i}",
//                    Description = $"This is a description for post {i}.",
//                    Stars = random.Next(1, 6), // Random stars between 1 and 5
//                    Images = new List<string>
//            {
//                $"https://plus.unsplash.com/premium_photo-1733230677536-ebd9121658ce?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxmZWF0dXJlZC1waG90b3MtZmVlZHwyfHx8ZW58MHx8fHx8", // Random Image 1
//                $"https://images.unsplash.com/photo-1733103373160-003dc53ccdba?q=80&w=1987&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
//                "https://images.unsplash.com/photo-1733247399489-7cb199fda872?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxmZWF0dXJlZC1waG90b3MtZmVlZHwxMnx8fGVufDB8fHx8fA%3D%3D",
//                 "https://plus.unsplash.com/premium_photo-1732569105984-a0fc4d858943?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxmZWF0dXJlZC1waG90b3MtZmVlZHwxOXx8fGVufDB8fHx8fA%3D%3D"
//            },
//                    time = DateTime.Now.AddMinutes(-random.Next(1, 100)).ToString("yyyy-MM-dd HH:mm:ss"),
//                    Author = $"Author {random.Next(1, 100)}",
//                    AuthorId = Guid.NewGuid().ToString(),
//                    likesCount = random.Next(0, 100),
//                    dislikesCount = random.Next(0, 20),
//                    commentsCount = random.Next(0, 50),
//                    shareCount = random.Next(0, 10)
//                });
//            }

//            return posts;
//        }

//    }
