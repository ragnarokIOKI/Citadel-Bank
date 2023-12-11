package com.example.bankandroid.Models;

public class Transaction {
    public int idTransaction ;
    public String nameTransaction ;
    public String dateTransaction ;
    public double summTransaction ;
    public String transactionAccountId ;
    public int transactionTypeId ;
    public boolean transactionDeleted ;

    public Transaction(int idTransaction, String nameTransaction, String dateTransaction, double summTransaction, String transactionAccountId, int transactionTypeId, boolean transactionDeleted) {
        this.idTransaction = idTransaction;
        this.nameTransaction = nameTransaction;
        this.dateTransaction = dateTransaction;
        this.summTransaction = summTransaction;
        this.transactionAccountId = transactionAccountId;
        this.transactionTypeId = transactionTypeId;
        this.transactionDeleted = transactionDeleted;
    }

    public int getIdTransaction() {
        return idTransaction;
    }

    public void setIdTransaction(int idTransaction) {
        this.idTransaction = idTransaction;
    }

    public String getNameTransaction() {
        return nameTransaction;
    }

    public void setNameTransaction(String nameTransaction) {
        this.nameTransaction = nameTransaction;
    }

    public String getDateTransaction() {
        return dateTransaction;
    }

    public void setDateTransaction(String dateTransaction) {
        this.dateTransaction = dateTransaction;
    }

    public double getSummTransaction() {
        return summTransaction;
    }

    public void setSummTransaction(double summTransaction) {
        this.summTransaction = summTransaction;
    }

    public String getTransactionAccountId() {
        return transactionAccountId;
    }

    public void setTransactionAccountId(String transactionAccountId) {
        this.transactionAccountId = transactionAccountId;
    }

    public int getTransactionTypeId() {
        return transactionTypeId;
    }

    public void setTransactionTypeId(int transactionTypeId) {
        this.transactionTypeId = transactionTypeId;
    }

    public boolean isTransactionDeleted() {
        return transactionDeleted;
    }

    public void setTransactionDeleted(boolean transactionDeleted) {
        this.transactionDeleted = transactionDeleted;
    }
}
