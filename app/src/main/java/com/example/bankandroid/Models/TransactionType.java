package com.example.bankandroid.Models;

public class TransactionType {
    public int idTransactionType;
    public String nameTransactionType;
    public boolean transactionTypeDeleted;

    public TransactionType(int idTransactionType, String nameTransactionType, boolean transactionTypeDeleted) {
        this.idTransactionType = idTransactionType;
        this.nameTransactionType = nameTransactionType;
        this.transactionTypeDeleted = transactionTypeDeleted;
    }

    public int getIdTransactionType() {
        return idTransactionType;
    }

    public void setIdTransactionType(int idTransactionType) {
        this.idTransactionType = idTransactionType;
    }

    public String getNameTransactionType() {
        return nameTransactionType;
    }

    public void setNameTransactionType(String nameTransactionType) {
        this.nameTransactionType = nameTransactionType;
    }

    public boolean isTransactionTypeDeleted() {
        return transactionTypeDeleted;
    }

    public void setTransactionTypeDeleted(boolean transactionTypeDeleted) {
        this.transactionTypeDeleted = transactionTypeDeleted;
    }
}
