#ifndef WIDGET_H
#define WIDGET_H

#include <QWidget>
#include <QLineEdit>
#include <QTextEdit>
#include <QPushButton>
#include <QVBoxLayout>
#include <QLabel>
#include <QInputDialog>
#include <QProcess>
#include <QSqlDatabase>
#include <QDebug>
#include <QSqlQuery>
#include <QSqlError>

class Widget : public QWidget
{
    Q_OBJECT

public:
    Widget(QWidget *parent = nullptr);
    ~Widget();

private:
    QSqlDatabase db;
    QSqlQuery *query;
    QLineEdit *nameLineEdit;
    QLineEdit *emailLineEdit;
    QTextEdit *booksTextEdit;
    QPushButton *addBookButton;
    QPushButton *clearButton;
    QPushButton *saveButton;
    QPushButton *openFileButton;
    QPushButton *bookReservationButton;

private slots:
    void addBook();
    void clearForm();
    void saveData();
    void openFile();
    void addBookFromArchive(const QString &bookTitle);
    void bookReservation();
    void displayReservationDetails();
};

#endif
