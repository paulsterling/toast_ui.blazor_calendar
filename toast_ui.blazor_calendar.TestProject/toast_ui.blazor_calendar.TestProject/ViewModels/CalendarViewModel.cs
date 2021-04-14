﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using toast_ui.blazor_calendar.Services;
using toast_ui.blazor_calendar.Models;
using Bogus;

namespace toast_ui.blazor_calendar.TestProject.ViewModels
{
    public class CalendarViewModel : BaseViewModel
    {

        private readonly TUICalendarInteropService CalendarService;
        public CalendarViewModel(TUICalendarInteropService calendarService)
        {
            CalendarService = calendarService;
        }

        private List<TUISchedule> _Schedules;
        public List<TUISchedule> Schedules
        {
            get => _Schedules;
            set
            {
                SetValue(ref _Schedules, value);
            }
        }

        private TUICalendarOptions _CalendarOptions;
        public TUICalendarOptions CalendarOptions
        {
            get => _CalendarOptions;
            set
            {
                SetValue(ref _CalendarOptions, value);
            }
        }

        private IEnumerable<TUICalendarProps> _CalendarProps;
        public IEnumerable<TUICalendarProps> CalendarProps
        {
            get => _CalendarProps;
            set
            {
                SetValue(ref _CalendarProps, value);
            }
        }

        private TUICalendarViewName _CalendarViewName;
        public TUICalendarViewName CalendarViewName
        {
            get => _CalendarViewName;
            set
            {
                SetValue(ref _CalendarViewName, value);
            }
        }


        public async Task InitCalendarDataAsync()
        {
            CalendarOptions = new TUICalendarOptions()
            {
                useCreationPopup = true,
                useDetailPopup = true,
            };
            
            var calendarProps = new List<TUICalendarProps>(); 
            var calendar1 = new TUICalendarProps()
            {
                id = "1",
                name = "My Test Calendar",
                color = "#ffffff",
                bgColor = "#9e5fff",
                dragBgColor = "#9e5fff",
                borderColor = "#9e5fff"
            };
            calendarProps.Add(calendar1);

            var calendar2 = new TUICalendarProps()
            {
                id = "2",
                name = "My Test Calendar2",
                color = "#00a9ff",
                bgColor = "#00a9ff",
                dragBgColor = "#00a9ff",
                borderColor = "#00a9ff"
            };
            calendarProps.Add(calendar2);
            CalendarProps = calendarProps;

            await Task.Run(() =>
            {
               _Schedules = new List<TUISchedule>();
               for (int i = 0; i < 50; i++)
               {
                   _Schedules.Add(GetFakeSchedule());
               }
            });
        }

        private TUISchedule GetFakeSchedule()
        {
            var faker = new Faker();
            
            var startDate = faker.Date.BetweenOffset(DateTimeOffset.Now.AddDays(-10), DateTimeOffset.Now.AddDays(10));
            var endDate = startDate.AddMinutes(faker.Random.Int(15, 300));
            var sched = new TUISchedule()
            {
                id = Guid.NewGuid().ToString(),
                calendarId = faker.Random.Int(1,2).ToString(),
                start = new TUITzDate() { _date =startDate },
                end = new TUITzDate() { _date = endDate },
                title = faker.Lorem.Sentence(faker.Random.Int(3,7)),
                body = faker.Lorem.Paragraph(3),
                category = "time",
                isVisible = true,
                isAllDay = false,
                state="busy"
                
            };

            return sched;

        }
    }
}
