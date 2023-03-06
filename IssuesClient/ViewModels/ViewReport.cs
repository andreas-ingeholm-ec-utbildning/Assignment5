using System;
using System.ComponentModel.DataAnnotations;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IssuesClient.Models;

namespace IssuesClient.ViewModels;

public partial class ViewReport : ViewModel
{

    [ObservableProperty] private bool m_isCommentBoxVisible;
    [ObservableProperty, Required] private string m_comment;
    [ObservableProperty, Required] private User m_user = null!;

    [RelayCommand]
    void OpenCommentBox() => IsCommentBoxVisible = true;

    [RelayCommand]
    void HideCommentBox() => IsCommentBoxVisible = false;

    public override string Title => "Issue browser - " + "Report";

    [RelayCommand]
    void AddComment()
    {

        if (Parameter is not Report report)
            throw new InvalidOperationException("Could not find report to add comment to.");

        report.Comments.Add(new() { User = User, Content = Comment });

    }

    [RelayCommand]
    void RemoveReport()
    {
        //TODO: Support removing report
    }

    [RelayCommand]
    void RemoveComment(Comment comment)
    {
        comment.IsRemoved = true;
        comment.Content = null;
        //TODO: Save
    }

}