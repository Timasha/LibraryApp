#include "widget.h"
#include <QMessageBox>
#include <QFileDialog>
#include <QFile>
#include <QTextStream>
#include <QRegularExpressionValidator>

Widget::Widget(QWidget *parent) : QWidget(parent)
{
    QRegExp emailRegex("\\b[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\\.[A-Z|a-z]{2,}\\b");
    QValidator *emailValidator = new QRegExpValidator(emailRegex, this);
    nameLineEdit = new QLineEdit(this);
    emailLineEdit = new QLineEdit(this);
    booksTextEdit = new QTextEdit(this);


    addBookButton = new QPushButton("Добавить книгу", this);
    bookReservationButton = new QPushButton("Забронировать книги", this);
    openFileButton = new QPushButton("Выбрать книги", this);
    clearButton = new QPushButton("Очистить", this);
    saveButton = new QPushButton("Сохранить", this);

    QVBoxLayout *layout = new QVBoxLayout(this);
    layout->addWidget(new QLabel("Client's name:", this));
    layout->addWidget(nameLineEdit);
    layout->addWidget(new QLabel("Email client:", this));
    layout->addWidget(emailLineEdit);
    layout->addWidget(new QLabel("List of books:", this));
    layout->addWidget(booksTextEdit);
    layout->addWidget(addBookButton);
    layout->addWidget(clearButton);
    layout->addWidget(saveButton);
    layout->addWidget(openFileButton);
    layout->addWidget(bookReservationButton);

    connect(addBookButton, &QPushButton::clicked, this, &Widget::addBook);
     connect(bookReservationButton, &QPushButton::clicked, this, &Widget::bookReservation);
    connect(openFileButton, &QPushButton::clicked, this, &Widget::openFile);
    connect(clearButton, &QPushButton::clicked, this, &Widget::clearForm);
   connect(saveButton, &QPushButton::clicked, this, &Widget::saveData);
   db=QSqlDatabase::addDatabase("QSQLITE");
   db.setDatabaseName("./testDB.db");
   if(db.open())
   {
       qDebug("open");
   }
   else
   {
       qDebug("no open");
   }
   query=new QSqlQuery(db);
   query->exec("CREATE TABLE IF NOT EXISTS BookReservations (Name TEXT, Email TEXT, Books TEXT);");
   displayReservationDetails();
}

Widget::~Widget()
{
}


void Widget::displayReservationDetails()
{
    QSqlQuery query("SELECT Name, Email, Books FROM BookReservations", db);

        if (query.exec())
        {
            while (query.next())
            {
                QString name = query.value(0).toString();
                QString email = query.value(1).toString();
                QString books = query.value(2).toString();

                qDebug() << "Name: " << name << ", Email: " << email << ", Books: " << books;
            }
        }
        else
        {
            qDebug() << "Ошибка при выполнении SQL-запроса: " << query.lastError().text();
        }
}

void Widget::bookReservation()
{


    QString name = nameLineEdit->text();
        QString email = emailLineEdit->text();
        QString books = booksTextEdit->toPlainText();

        if (name.isEmpty() || email.isEmpty() || books.isEmpty())
        {
            qDebug() << "Введите все необходимые данные перед бронированием.";
            return;
        }

        query->prepare("INSERT INTO BookReservations (Name, Email, Books) VALUES (:name, :email, :books);");
        query->bindValue(":name", name);
        query->bindValue(":email", email);
        query->bindValue(":books", books);

        if (query->exec())
        {
            qDebug("Детали бронирования добавлены в базу данных.");

            displayReservationDetails();
        }
        else
        {
            qDebug() << "Ошибка при вставке деталей бронирования: " << query->lastError().text();
        }
    QString reservationFileName = QFileDialog::getSaveFileName(this, "Забронировать книги", "", "PDF Files (*.pdf);;All Files (*)");

    if (!reservationFileName.isEmpty())
    {
        QFile reservationFile(reservationFileName);

        if (reservationFile.open(QIODevice::WriteOnly | QIODevice::Text))
        {
            QTextStream out(&reservationFile);
            out.setCodec("UTF-8");

            out << "Reservation Details:\n";
            out << "Name: " << nameLineEdit->text() << "\n";
            out << "Email: " << emailLineEdit->text() << "\n";
            out << "Reserved Books:\n" << booksTextEdit->toPlainText();

            reservationFile.close();

            QMessageBox::information(this, "Забронировано", "Книги успешно забронированы!");
        }
        else
        {
            QMessageBox::critical(this, "Ошибка", "Не удалось открыть файл для записи бронирования!");
        }
    }
}

void Widget::addBookFromArchive(const QString &bookTitle)
{
    if (!bookTitle.isEmpty())
    {
        QString currentText = booksTextEdit->toPlainText();
        if (!currentText.isEmpty())
        {
            currentText += "\n";
        }
        currentText += bookTitle;
        booksTextEdit->setPlainText(currentText);
    }
}

void Widget::addBook()
{
    QString fileName = QFileDialog::getOpenFileName(this, "Выбрать книгу", "", "PDF Files (*.pdf);;All Files (*)");

    if (!fileName.isEmpty())
    {
        QString bookTitle = QFileInfo(fileName).fileName();

        addBookFromArchive(bookTitle);

        QProcess::startDetached("xdg-open", QStringList() << fileName);
    }
}

void Widget::clearForm()
{
    nameLineEdit->clear();
    emailLineEdit->clear();
    booksTextEdit->clear();
}
void Widget::openFile()
{
    QString fileName = QFileDialog::getOpenFileName(this, "Выбрать книги", "", "PDF Files (*.pdf);;All Files (*)");

    if (!fileName.isEmpty())
    {
        QFile file(fileName);
        if (file.open(QIODevice::ReadOnly | QIODevice::Text))
        {
            QTextStream in(&file);
            QString books = in.readAll();
            booksTextEdit->setPlainText(books);
            file.close();
        }
        else
        {
            QMessageBox::critical(this, "Ошибка", "Не удалось открыть файл для чтения!");
        }
    }
}

void Widget::saveData()
{
    QString name = nameLineEdit->text();
    QString email = emailLineEdit->text();
    QString books = booksTextEdit->toPlainText();

    if (name.isEmpty() || email.isEmpty() || books.isEmpty() ||
        !nameLineEdit->hasAcceptableInput() || !emailLineEdit->hasAcceptableInput())
    {
        QMessageBox::warning(this, "Предупреждение", "Заполните все поля корректно перед сохранением!");
        return;
    }

    QRegularExpression emailRegex("\\b[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\\.[A-Z|a-z]{2,}\\b");
    if (!emailRegex.match(email).hasMatch())
    {
        QMessageBox::warning(this, "Предупреждение", "Введите корректный email!");
        return;
    }

    QString fileName = QFileDialog::getSaveFileName(this, "Сохранить данные", "", "Text Files (*.txt);;All Files (*)");

    if (!fileName.isEmpty())
    {
        QFile file(fileName);

        if (file.open(QIODevice::WriteOnly | QIODevice::Text))
        {
            QTextStream out(&file);
            out.setCodec("UTF-8");
            out << "Name: " << name << "\n";
            out << "Email: " << email << "\n";
            out << "Books:\n" << books;
            file.close();

            QMessageBox::information(this, "Сохранено", "Данные успешно сохранены в файл!");
        }
        else
        {
            QMessageBox::critical(this, "Ошибка", "Не удалось открыть файл для записи!");
        }
    }

    query->prepare("INSERT INTO BookReservations (Name, Email, Books) VALUES (:name, :email, :books);");
    query->bindValue(":name", name);
    query->bindValue(":email", email);
    query->bindValue(":books", books);

    if (query->exec())
    {
        qDebug("Данные успешно сохранены в базе данных!");
    }
    else
    {
        qDebug() << "Ошибка при вставке данных в базу данных: " << query->lastError().text();
    }

    QMessageBox::information(this, "Сохранено", "Данные успешно сохранены!");
}
