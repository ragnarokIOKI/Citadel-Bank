package com.example.bankandroid.Models;

import com.google.gson.annotations.SerializedName;

public class Card {
    @SerializedName("cardNumber")
    public String IdCard;
    @SerializedName("cardHolder")
    public String CardHolder;
    @SerializedName("validity")
    public String Validity;
    @SerializedName("ccv")
    public String Ccv;
    @SerializedName("cardAccountId")
    public String CardAccountId;
    @SerializedName("cardDeleted")
    public boolean CardDeleted;
    @SerializedName("card_Deletion_Reason")
    public String card_Deletion_Reason;

    public Card() {
    }

    public String getCard_Deletion_Reason() {
        return card_Deletion_Reason;
    }
    public void setCard_Deletion_Reason(String card_Deletion_Reason) {
        this.card_Deletion_Reason = card_Deletion_Reason;
    }
    public String getIdCard() {
        return IdCard;
    }

    public void setIdCard(String idCard) {
        IdCard = idCard;
    }

    public String getCardHolder() {
        return CardHolder;
    }

    public void setCardHolder(String cardHolder) {
        CardHolder = cardHolder;
    }

    public String getValidity() {
        return Validity;
    }

    public void setValidity(String validity) {
        Validity = validity;
    }

    public String getCcv() {
        return Ccv;
    }

    public void setCcv(String ccv) {
        Ccv = ccv;
    }

    public String getCardAccountId() {
        return CardAccountId;
    }

    public void setCardAccountId(String cardAccountId) {
        CardAccountId = cardAccountId;
    }

    public boolean isCardDeleted() {
        return CardDeleted;
    }

    public void setCardDeleted(boolean cardDeleted) {
        CardDeleted = cardDeleted;
    }

    public Card(String cardNumber, String cardHolder, String validity, String ccv, String cardAccountId, boolean cardDeleted, String card_Deletion_Reason) {
        IdCard = cardNumber;
        CardHolder = cardHolder;
        Validity = validity;
        Ccv = ccv;
        CardAccountId = cardAccountId;
        CardDeleted = cardDeleted;
        this.card_Deletion_Reason = card_Deletion_Reason;
    }
}
