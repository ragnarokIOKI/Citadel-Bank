package com.example.bankandroid.Utility;

import android.app.Application;

import com.example.bankandroid.Models.*;

import java.util.ArrayList;
import java.util.Date;
import java.util.List;

import retrofit2.Call;
import retrofit2.http.Body;
import retrofit2.http.GET;
import retrofit2.http.Header;
import retrofit2.http.POST;
import retrofit2.http.PUT;
import retrofit2.http.Path;
import retrofit2.http.Query;

public interface ApiInterface {
    //Users
    @POST("LogIn")
    Call<Key> login(
            @Query("login") String login,
            @Query("password") String pass
    );
    @POST("api/Users/refresh_token")
    Call<Key> refresh(
            @Query("access_token") String access_token);
    @POST("Authorize")
    Call<User> auth(
            @Body User user
    );
    @GET("api/Users/{id}")
    Call<User> getUserBy(@Path("id") int id);
    @GET("api/Users")
    Call<ArrayList<User>> getUsers();
    @PUT("api/Users/{id}")
    Call<User> putUser(
            @Path("id") int id,
            @Body User user
    );

    //Account
    @GET("api/Accounts")
    Call<ArrayList<Account>> getAccount(@Query("searchuser") int searchuser, @Query ("orderBy") boolean orderBy);
    @GET("api/Accounts/{id}")
    Call<Account> getAccountby(@Path("id") int id);
    @PUT("api/Accounts/AccountsIDDelete")
    Call<ArrayList<Account>> deleteAccounts(@Body String[] id, @Query("deletionReason") String deletionReason);
    @PUT("api/Accounts/AccountsIDReturn")
    Call<ArrayList<Account>> returnAccounts(@Body String[] id);

    //Card
    @GET("api/Cards")
    Call<ArrayList<Card>> getCard(@Query("searchAccountNum") String searchAccountNum, @Query ("orderBy") Boolean orderBy);
    @GET("api/Cards/{id}")
    Call<Card> getCardby(@Path("id") int id);
    @PUT("api/Cards/CardsIDDelete")
    Call<ArrayList<Card>> deleteCards(@Body String[] id, @Query("deletionReason") String deletionReason);
    @PUT("api/Cards/CardsIDReturn")
    Call<ArrayList<Card>> returnCards(@Body String[] id);


    //AccountType
    @GET("api/AccountTypes")
    Call<ArrayList<AccountType>> getAccountType();
    @GET("api/AccountTypes/{id}")
    Call<AccountType> getAccountTypeby(@Path("id") int id);

    //AccountApplication
    @GET("api/AccountApplications")
    Call<ArrayList<AccountApplication>> getAccountApplication(@Query("searchuser") int searchuser, @Query("orderBy") int orderBy);
    @GET("api/AccountApplications/{id}")
    Call<AccountApplication> getAccountApplicationby(@Path("id") int id);
    @POST("api/AccountApplications")
    Call<AccountApplication> postAccountApplication(
            //@Header("Authorization") String access_token,
            @Body AccountApplication accountApplication
    );
    @PUT("api/AccountApplications")
    Call<AccountApplication> putAccountApplications(
            //@Header("Authorization") String access_token,
            @Path("id") int id,
            @Body AccountApplication accountApplication
    );

    //ApplicationStatus
    @GET("api/ApplicationStatus")
    Call<ArrayList<ApplicationStatus>> getAppStat();
    @GET("api/ApplicationStatus/{id}")
    Call<ApplicationStatus> getAppStatby(@Path("id") int id);

    //CreditApplication
    @GET("api/CreditApplications")
    Call<ArrayList<CreditApplication>> getCreditApplication(@Query("searchuser") int searchuser, @Query("orderBy") int orderBy, @Query("searchAmount") String searchAmount);
    @GET("api/CreditApplications/{id}")
    Call<CreditApplication> getCreditApplicationby(@Path("id") int id);
    @POST("api/CreditApplications")
    Call<CreditApplication> postCreditApplication(
            //@Header("Authorization") String access_token,
            @Body CreditApplication creditApplication
    );

    //CreditAgreement
    @GET("api/CreditAgreements")
    Call<ArrayList<CreditAgreement>> getCreditAgreements(@Query("searchuser") int searchuser, @Query("searchbyApplication") String searchbyApplication, @Query("orderBy") Boolean orderBy);
    @GET("api/CreditAgreements/{id}")
    Call<CreditAgreement> getCreditAgreementby(@Path("id") int id);

    //Transaction
    @GET("api/Transactions")
    Call<ArrayList<Transaction>> getTransactions(@Query("searchAcc") String searchAcc, @Query("orderbyTranType") int orderbyTranType, @Query("orderBy") Boolean orderBy);
    @GET("api/Transactions/{id}")
    Call<Transaction> getTransactionby(@Path("id") int id);

    //TransactionType
    @GET("api/TransactionTypes/{id}")
    Call<TransactionType> getTrTypeby(@Path("id") int id);

    //CalculateOverpayment
    @GET("api/BackUp/CalculateOverpayment")
    Call<OverpaymentDTO> getOverpayment(@Query("creditAmount") double creditAmount,
                                        @Query("creditRate") double creditRate,
                                        @Query("dateDrawing") String dateDrawing,
                                        @Query("dateTermination") String dateTermination);

}
