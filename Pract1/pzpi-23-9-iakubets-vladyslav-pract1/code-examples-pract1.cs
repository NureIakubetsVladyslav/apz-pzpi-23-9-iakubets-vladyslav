using System;

namespace DecoratorPatternExample
{
    // Базовий інтерфейс
    public interface IMessage
    {
        string GetText();
    }

    // Конкретний компонент
    public class SimpleMessage : IMessage
    {
        public string GetText()
        {
            return "Базове повiдомлення";
        }
    }

    // Абстрактний декоратор
    public abstract class MessageDecorator : IMessage
    {
        protected IMessage message;

        public MessageDecorator(IMessage message)
        {
            this.message = message;
        }

        public virtual string GetText()
        {
            return message.GetText();
        }
    }

    // Конкретний декоратор A
    public class ImportantMessageDecorator : MessageDecorator
    {
        public ImportantMessageDecorator(IMessage message) : base(message)
        {
        }

        public override string GetText()
        {
            return "[Важливо] " + base.GetText();
        }
    }

    // Конкретний декоратор B
    public class EncryptedMessageDecorator : MessageDecorator
    {
        public EncryptedMessageDecorator(IMessage message) : base(message)
        {
        }

        public override string GetText()
        {
            return base.GetText() + " [Зашифровано]";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            IMessage message = new SimpleMessage();
            Console.WriteLine(message.GetText());

            IMessage importantMessage = new ImportantMessageDecorator(message);
            Console.WriteLine(importantMessage.GetText());

            IMessage encryptedImportantMessage =
                new EncryptedMessageDecorator(importantMessage);
            Console.WriteLine(encryptedImportantMessage.GetText());
        }
    }
}
