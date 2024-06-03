using Microsoft.AspNetCore.Mvc;
using Microsoft.SemanticKernel.ChatCompletion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemanticKernelMicroservice.Infrastructure.ChatHistoryService
{
    // Register
    //services.AddSingleton<IChatHistoryService, ChatHistoryService>();

    // Get chat history in application
    //private readonly IChatHistoryService _chatHistoryService;

    //public MyCommand(IChatHistoryService chatHistoryService)
    //{
    //    _chatHistoryService = chatHistoryService;
    //}

    //public chatHistory GetChatHistory(string sessionId)
    //{
    //    var chatHistory = _chatHistoryService.GetChatHistory(sessionId);
    //    // Return chat history as needed
    //    return chatHistory;
    //}

    //public void SaveChatHistory(string sessionId, ChatHistory history)
    //{
    //    _chatHistoryService.SaveChatHistory(sessionId, history);
    //}

    // IChatHistoryService.cs
    public interface IChatHistoryService
    {
        ChatHistory GetChatHistory(string sessionId);
        void SaveChatHistory(string sessionId, ChatHistory history);
    }

    // ChatHistoryService.cs
    public class ChatHistoryService : IChatHistoryService
    {
        private readonly Dictionary<string, ChatHistory> _userSessionChatHistories;

        public ChatHistoryService()
        {
            _userSessionChatHistories = new Dictionary<string, ChatHistory>();
        }

        public ChatHistory GetChatHistory(string sessionId)
        {
            return _userSessionChatHistories.TryGetValue(sessionId, out var chatHistory)
                ? chatHistory
                : new ChatHistory(); // Create a new one if not found
        }

        public void SaveChatHistory(string sessionId, ChatHistory history)
        {
            _userSessionChatHistories[sessionId] = history;
        }
    }

}
