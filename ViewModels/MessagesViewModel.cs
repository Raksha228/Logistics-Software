using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Backend.Models;
using Backend.Services;
using System.Collections.ObjectModel;
using System;
using Logistics_Software.Services;
using Microsoft.VisualBasic;

namespace Logistics_Software.ViewModels
{
    public partial class MessagesViewModel : ObservableObject
    {
        private readonly MessageService _messageService;
        private readonly int _currentUserId; // должен передаваться извне при инициализации (через конструктор или DI)

        public MessagesViewModel(int userId)
        {
            _currentUserId = userId;
            _messageService = new MessageService();
            LoadConversations();
        }

        [ObservableProperty]
        private ObservableCollection<Conversation> conversations;

        [ObservableProperty]
        private Conversation selectedConversation;

        [ObservableProperty]
        private ObservableCollection<Message> messages;

        [ObservableProperty]
        private string newMessageText;

        [RelayCommand]
        private async void LoadConversations()
        {
            var convos = await _messageService.GetUserConversationsAsync(_currentUserId);
            Conversations = new ObservableCollection<Conversation>(convos);
        }

        [RelayCommand]
        private async void LoadMessages()
        {
            if (SelectedConversation == null) return;

            var msgs = await _messageService.GetMessagesByConversationIdAsync(SelectedConversation.Id);
            Messages = new ObservableCollection<Message>(msgs);
        }

        [RelayCommand]
        private async void SendMessage()
        {
            if (string.IsNullOrWhiteSpace(NewMessageText) || SelectedConversation == null)
                return;

            var message = new Message
            {
                ConversationId = SelectedConversation.Id,
                SenderId = _currentUserId,
                Content = NewMessageText,
                Timestamp = DateTime.Now
            };

            var result = await _messageService.SendMessageAsync(message);
            if (result)
            {
                Messages.Add(message);
                NewMessageText = string.Empty;
            }
        }
    }
}
