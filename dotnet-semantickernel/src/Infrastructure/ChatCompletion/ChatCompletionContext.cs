//using Microsoft.SemanticKernel.ChatCompletion;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.ServiceModel.Channels;
//using System.Text;
//using System.Threading.Tasks;

//namespace SemanticKernelMicroservice.Infrastructure.ChatCompletion
//{
//    // Define your models (User, Message, ChatSession) and DbContext
//    public class ChatDbContext : DbContext
//    {
//        // Roles: User, Assistant, System
//        public DbSet<User> Users { get; set; }
//        public DbSet<Message> Messages { get; set; }
//        public DbSet<ChatSession> ChatSessions { get; set; }

//        // Configure connection string and other options

//        protected override void OnModelCreating(ModelBuilder modelBuilder)
//        {
//            // Define relationships, indexes, etc.
//        }
//    }

//    // Example usage
//    public class ChatService
//    {
//        private readonly ChatDbContext _dbContext;

//        public ChatService(ChatDbContext dbContext)
//        {
//            _dbContext = dbContext;
//        }

//        public ChatHistory LoadChatHistory(string sessionId)
//        {
//            var chatHistory = new ChatHistory();
//            var messages = _dbContext.Messages
//                .Where(m => m.ChatSessionId == sessionId)
//                .OrderBy(m => m.Timestamp)
//                .ToList();

//            foreach (var message in messages)
//            {
//                chatHistory.AddUserMessage(message.Content);
//                // Add other relevant info (e.g., system messages, assistant replies)
//            }

//            return chatHistory;
//        }

//        public void AddUserMessage(string sessionId, string content)
//        {
//            // Save the user message to the database
//            var message = new Message { ChatSessionId = sessionId, Content = content };
//            _dbContext.Messages.Add(message);
//            _dbContext.SaveChanges();
//        }
//    }

//}
