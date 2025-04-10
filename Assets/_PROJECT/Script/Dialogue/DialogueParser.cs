using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

namespace DIALOGUE
{
    public class DialogueParser
    {
        private const string commandRegexPattern = "\\w*[^\\s]\\(";

        public static DIALOGUE_LINE Parse(string rawLine)
        {
            Debug.Log($"Parsing line: '{rawLine}'");
            (string speaker, string dialogue, string commands) = RipContent(rawLine);

            // Format the text with rich text tags for color and size
            string formattedText = $"<color=#C8C4C1><size=20>{speaker}</size></color>\n<color=#FFFFFF><size=24>{dialogue}</size></color>";
            string formattedSpeaker = $"<color=#C8C4C1><size=20>{speaker}</size></color>";
            string formattedDialogue = $"<color=#FFFFFF><size=24>{dialogue}</size></color>";
            
            Debug.Log($"Formatted Text = '{formattedText}'");
            return new DIALOGUE_LINE(formattedSpeaker, formattedDialogue, commands);

            // Debug.Log($"Speaker = '{speaker}'\nDialogue = '{dialogue}'\nCommands = '{commands}'");
            // return new DIALOGUE_LINE(speaker, dialogue, commands);
        }

        private static (string, string, string) RipContent(string rawLine)
        {
            string speaker = "", dialogue = "", commands = "";

            int dialogueStart = -1;
            int dialogueEnd = -1;
            bool isEscaped = false;

            for(int i = 0; i < rawLine.Length; i++)
            {
                char current = rawLine[i];
                if (current == '\\')
                    isEscaped = !isEscaped;
                else if (current == '"' && !isEscaped)
                {
                    if (dialogueStart == -1)
                        dialogueStart = i;
                    else if (dialogueEnd == -1)
                        dialogueEnd = i;
                }
                else
                    isEscaped = false;
            }

            //identify command pattern
            Regex commandRegex = new Regex(commandRegexPattern);
            Match match = commandRegex.Match(rawLine);
            int commandStart = -1;
            if (match.Success)
            {
                commandStart = match.Index;

                if (dialogueStart == -1 && dialogueEnd == -1)
                    return ("", "", rawLine.Trim());
            }

            // if either have dialogue or a multi word argument in a command. Figure out if this is dialog
            if (dialogueStart != -1 && dialogueEnd != -1 && (commandStart == -1 || commandStart > dialogueEnd))
            {
                // we have valid dialogue
                speaker = rawLine.Substring(0, dialogueStart).Trim();
                dialogue = rawLine.Substring(dialogueStart + 1, dialogueEnd - dialogueStart - 1).Replace("\\\"", "\"");

                if (commandStart != -1)
                    commands = rawLine.Substring(commandStart).Trim();
            }
            else if (commandStart != -1 && dialogueStart > commandStart)
                commands = rawLine;
            else
                speaker = rawLine;
            
            return (speaker, dialogue, commands);
        }
    }
}