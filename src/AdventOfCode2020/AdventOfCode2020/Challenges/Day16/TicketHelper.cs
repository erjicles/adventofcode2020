using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2020.Challenges.Day16
{
    public static class TicketHelper
    {
        public static Dictionary<string, int> GetFieldIndexes(TicketData ticketData)
        {
            var result = new Dictionary<string, int>();
            var validTickets = GetNearbyTicketsWithNoInvalidValues(ticketData);
            var indexTicketFieldCandidates = new Dictionary<int, IList<TicketField>>();
            for (int fieldIndex = 0; fieldIndex < ticketData.TicketFields.Count; fieldIndex++)
            {
                var possibleTicketFields = GetTicketFieldsThatWorkForIndex(
                    fieldIndex, 
                    validTickets, 
                    ticketData.TicketFields);
                indexTicketFieldCandidates[fieldIndex] = possibleTicketFields;
            }
            while (indexTicketFieldCandidates.Count > 0)
            {
                var currentCandidate = indexTicketFieldCandidates
                    .OrderBy(kvp => kvp.Value.Count)
                    .First();
                if (currentCandidate.Value.Count != 1)
                {
                    throw new Exception("Non-deterministic candidate found");
                }
                var fieldIndex = currentCandidate.Key;
                var ticketField = currentCandidate.Value[0];
                result[ticketField.Name] = fieldIndex;
                indexTicketFieldCandidates.Remove(fieldIndex);
                foreach (var kvp in indexTicketFieldCandidates)
                {
                    kvp.Value.Remove(ticketField);
                }
                foreach (var kvp in indexTicketFieldCandidates)
                {
                    if (kvp.Value.Count == 0)
                        indexTicketFieldCandidates.Remove(kvp.Key);
                }
            }
            
            return result;
        }

        public static IList<TicketField>  GetTicketFieldsThatWorkForIndex(
            int fieldIndex,
            IList<Ticket> tickets, 
            IList<TicketField> ticketFields)
        {
            var result = new List<TicketField>();
            foreach (var ticketField in ticketFields)
            {
                bool isTicketFieldValidForIndex = true;
                foreach (var ticket in tickets)
                {
                    var value = ticket.FieldValues[fieldIndex];
                    var isValid = GetIsValidValue(value, ticketField);
                    if (!isValid)
                    {
                        isTicketFieldValidForIndex = false;
                        break;
                    }
                }
                if (isTicketFieldValidForIndex)
                {
                    result.Add(ticketField);
                }
            }
            return result;
        }

        public static IList<Ticket> GetNearbyTicketsWithNoInvalidValues(TicketData ticketData)
        {
            var result = new List<Ticket>();
            foreach (var nearbyTicket in ticketData.NearbyTickets)
            {
                bool isInvalidTicket = false;
                foreach (var value in nearbyTicket.FieldValues)
                {
                    var isValid = GetIsValidValueForAnyField(value, ticketData.TicketFields);
                    if (!isValid)
                    {
                        isInvalidTicket = true;
                        break;
                    }
                }
                if (!isInvalidTicket)
                {
                    result.Add(nearbyTicket);
                }
            }
            return result;
        }

        public static int GetTicketScanningErrorRateForInvalidValues(IList<int> invalidValues)
        {
            var result = invalidValues.Sum();
            return result;
        }

        public static IList<int> GetInvalidNearbyTicketValuesForAllFields(TicketData ticketData)
        {
            var result = new List<int>();
            foreach (var nearbyTicket in ticketData.NearbyTickets)
            {
                foreach (var value in nearbyTicket.FieldValues)
                {
                    var isValid = GetIsValidValueForAnyField(value, ticketData.TicketFields);
                    if (!isValid)
                    {
                        result.Add(value);
                    }
                }
            }
            return result;
        }

        public static bool GetIsValidValueForAnyField(int value, IList<TicketField> ticketFields)
        {
            foreach (var ticketField in ticketFields)
            {
                var isValid = GetIsValidValue(value, ticketField);
                if (isValid)
                    return true;
            }
            return false;
        }

        public static bool GetIsValidValue(int value, TicketField ticketField)
        {
            foreach (var range in ticketField.Ranges)
            {
                if (value >= range.Item1 && value <= range.Item2)
                    return true;
            }
            return false;
        }

        public static TicketData ParseInputLines(IList<string> inputLines)
        {
            var ticketFieldInputLines = new List<string>();
            var yourTicketInputLine = string.Empty;
            var nearbyTicketInputLines = new List<string>();
            bool isProcessingYourTicket = false;
            bool isProcessingNearbyTickets = false;
            foreach (var inputLine in inputLines)
            {
                if (string.IsNullOrWhiteSpace(inputLine))
                    continue;
                if (isProcessingYourTicket)
                {
                    if ("nearby tickets:".Equals(inputLine))
                    {
                        isProcessingYourTicket = false;
                        isProcessingNearbyTickets = true;
                        continue;
                    }
                    yourTicketInputLine = inputLine;
                }
                else if (isProcessingNearbyTickets)
                {
                    nearbyTicketInputLines.Add(inputLine);
                }
                else
                {
                    if ("your ticket:".Equals(inputLine))
                    {
                        isProcessingYourTicket = true;
                        continue;
                    }
                    ticketFieldInputLines.Add(inputLine);
                }
            }

            var ticketFields = ParseTicketFieldInputLines(ticketFieldInputLines);
            var yourTicket = ParseTicketInputLine(yourTicketInputLine);
            var nearbyTickets = ParseTicketInputLines(nearbyTicketInputLines);
            var result = new TicketData(ticketFields, yourTicket, nearbyTickets);
            return result;
        }

        public static IList<TicketField> ParseTicketFieldInputLines(IList<string> ticketFieldInputLines)
        {
            var result = new List<TicketField>();
            foreach (var ticketFieldInputLine in ticketFieldInputLines)
            {
                var ticketField = ParseTicketFieldInputLine(ticketFieldInputLine);
                result.Add(ticketField);
            }
            return result;
        }

        public static IList<Ticket> ParseTicketInputLines(IList<string> ticketInputLines)
        {
            var result = new List<Ticket>();
            foreach (var ticketInputLine in ticketInputLines)
            {
                var ticket = ParseTicketInputLine(ticketInputLine);
                result.Add(ticket);
            }
            return result;
        }

        public static Ticket ParseTicketInputLine(string ticketInputLine)
        {
            var fieldValues = ticketInputLine
                .Split(",")
                .Select(fieldValueString => int.Parse(fieldValueString))
                .ToList();
            var result = new Ticket(fieldValues);
            return result;
        }

        public static TicketField ParseTicketFieldInputLine(string ticketFieldInputLine)
        {
            var match = Regex.Match(ticketFieldInputLine, @"^([^:]+): (\d+)-(\d+) or (\d+)-(\d+)$");
            if (!match.Success)
            {
                throw new Exception($"Invalid ticket field input line: {ticketFieldInputLine}");
            }
            var name = match.Groups[1].Value;
            var range1Min = int.Parse(match.Groups[2].Value);
            var range1Max = int.Parse(match.Groups[3].Value);
            var range2Min = int.Parse(match.Groups[4].Value);
            var range2Max = int.Parse(match.Groups[5].Value);
            var ranges = new List<Tuple<int, int>>()
            {
                new Tuple<int, int>(range1Min, range1Max),
                new Tuple<int, int>(range2Min, range2Max)
            };
            var result = new TicketField(name, ranges);
            return result;
        }
    }
}
