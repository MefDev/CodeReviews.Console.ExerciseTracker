﻿using System.Globalization;
using Spectre.Console;

namespace ExerciseTracker.Mefdev.UserInputs;

public class UserInput
{
    public UserInput()
    {

    }

    public string GetComment(string oldName = "")
    {
        return AnsiConsole
        .Prompt(new TextPrompt<string>($"Enter a comment ({oldName}): ")
        .Validate(name =>
        {
            if (string.IsNullOrWhiteSpace(name))
                return ValidationResult.Error("[red]comment cannot be empty![/]");
            return ValidationResult.Success();
        }));
    }

    public string GetType(string oldName = "")
    {
        return AnsiConsole
        .Prompt(new TextPrompt<string>($"Enter exercise type ({oldName}): ")
        .Validate(name =>
        {
            if (string.IsNullOrWhiteSpace(name))
                return ValidationResult.Error("[red]type cannot be empty![/]");
            return ValidationResult.Success();
        }));
    }

    public DateTime GetStartDate(string oldDate = "")
    {
        return GetDate("Enter the start date and time", oldDate);
    }

    public DateTime GetEndDate(string oldDate = "")
    {
        return GetDate("Enter the end date and time", oldDate);
    }

    private DateTime GetDate(string promptMessage, string oldDate = "")
    {
        while (true)
        {
            string dateInput = AnsiConsole.Prompt(
                new TextPrompt<string>($"{promptMessage} in the format [yellow] DD/MM/YYYY HH:mm:ss[/] ({oldDate}): ")
                .Validate(input =>
                {
                    return DateTime.TryParseExact(input, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out _)
                        ? ValidationResult.Success()
                        : ValidationResult.Error("[red]Invalid format! Please try again.[/]");
                }));

            if (DateTime.TryParseExact(dateInput, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDate))
            {
                return parsedDate;
            }
            AnsiConsole.MarkupLine("[red]Invalid date entered. Please try again.[/]");
        }
    }
}