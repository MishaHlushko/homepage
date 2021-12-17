using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Musical_Instrument_Store.Data.Models;
namespace Musical_Instrument_Store.Data
{
    public class DBObjects
    {
        public static void Initial(AppDBContext appDBContext)
        {
            
                

            if(!appDBContext.mICategories.Any())
            {
                appDBContext.mICategories.AddRange(MICategories.Select(c => c.Value));
            }

            if(!appDBContext.MusicalInstruments.Any())
            {
                appDBContext.AddRange(
                    new MusicalInstrument
                    {
                        nameMI = "ALESIS TURBO MESH KIT",
                        descMI = "Alesis Turbo Mesh Kit - це повноцінна барабанна установка з 8 сітковим педом малого барабана, трьома 8 сітковими педами томів, трьома 10 педами тарілок, розробленими Alesis педалями хай-хета і бас-барабана і міцною і легкою алюмінієвою рамою. Все необхідне йде в комплекті - просто збери установку та почни грати!",
                        img = "/img/turbo-mesh-kit-ortho-web.jpg",
                        price = 11760,
                        available = true,
                        MICategory = MICategories["Drums"]
                    },
                    new MusicalInstrument
                    {
                        nameMI = "CORT EARTH 70",
                        descMI = "Інструменти серії Earth, що зберегли зовнішній вигляд і звучання з часів золотої ери акустичних гітар, є чудовою альтернативою вінтажним акустикам. Гітари Earth чудово підходять як для експресивної гри медіатором, так і для делікатної пальцевої техніки.",
                        img = "/img/Earth70-op-front-2019-cort.jpg",
                        price = 5696,
                        available = true,
                        MICategory = MICategories["String"]
                    },
                    new MusicalInstrument
                    {
                        nameMI = "YAMAHA DTX6K2-X",
                        descMI = "Тембри акустичних барабанів в бібліотеці DTX-PRO - це звучання справжніх барабанів, записане в кращих студіях світу. Більш того, крім direct записів самих барабанів, ми також записали натуральну реверберацію приміщень студій, в яких були записані ці барабани.",
                        img = "/img/DTX6K2-X.jpg",
                        price = 54792,
                        available = true,
                        MICategory = MICategories["Drums"]
                    },
                    new MusicalInstrument
                    {
                        nameMI = "YAMAHA FG820",
                        descMI = "З'явившись в 1966 році, гітари Yamaha серії FG довели, що відмінна акустична гітара не обов'язково повинна бути дорогою. Створена з акцентом на зручність гри, музичне звучання і, звичайно ж чудову якість виготовлення, оригінальна FG180 дуже швидко стала справжньою класикою, що має в своєму послужному списку безліч хітових записів.",
                        img = "/img/fg820-bs-main-yamahajpg.jpg",
                        price = 11130,
                        available = true,
                        MICategory = MICategories["String"]
                    }
                );
            }
            appDBContext.SaveChanges();
        }
        private static Dictionary<string, MICategory> category;
        public static Dictionary<string, MICategory> MICategories
        {
            get
            {
                if(category == null)
                {
                    var list = new MICategory[]
                    {
                        new MICategory{ categoryName="Drums"},
                        new MICategory{ categoryName= "String"}
                    };

                    category = new Dictionary<string, MICategory>();
                    foreach(MICategory el in list)
                    {
                        category.Add(el.categoryName,el);
                    }
                }
                return category;
            }
        }
    }
}
