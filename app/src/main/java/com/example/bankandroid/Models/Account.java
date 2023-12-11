package com.example.bankandroid.Models;

import com.google.gson.annotations.SerializedName;

public class Account {
    @SerializedName("idAccount")
    public String IdAccount;
    @SerializedName("balance")
    public double _Balance;
    @SerializedName("percent")
    public int Percent;
    @SerializedName("typeId")
    public int TypeId;
    @SerializedName("userAccountId")
    public int UserAccountId;
    @SerializedName("accountDeleted")
    public boolean AccountDeleted;
    @SerializedName("account_Deletion_Reason")
    public String account_Deletion_Reason;

    public Account() {
    }

    public Account(String idAccount, double _Balance, int percent, int typeId, int userAccountId, boolean accountDeleted, String account_Deletion_Reason) {
        IdAccount = idAccount;
        this._Balance = _Balance;
        Percent = percent;
        TypeId = typeId;
        UserAccountId = userAccountId;
        AccountDeleted = accountDeleted;
        this.account_Deletion_Reason = account_Deletion_Reason;
    }

    public String getIdAccount() {
        return IdAccount;
    }

    public void setIdAccount(String idAccount) {
        IdAccount = idAccount;
    }

    public double get_Balance() {
        return _Balance;
    }

    public void set_Balance(double _Balance) {
        this._Balance = _Balance;
    }

    public int getPercent() {
        return Percent;
    }

    public void setPercent(int percent) {
        Percent = percent;
    }

    public int getTypeId() {
        return TypeId;
    }

    public void setTypeId(int typeId) {
        TypeId = typeId;
    }

    public int getUserAccountId() {
        return UserAccountId;
    }

    public void setUserAccountId(int userAccountId) {
        UserAccountId = userAccountId;
    }

    public boolean isAccountDeleted() {
        return AccountDeleted;
    }

    public void setAccountDeleted(boolean accountDeleted) {
        AccountDeleted = accountDeleted;
    }

    public String getAccount_Deletion_Reason() {
        return account_Deletion_Reason;
    }

    public void setAccount_Deletion_Reason(String account_Deletion_Reason) {
        this.account_Deletion_Reason = account_Deletion_Reason;
    }
}
