package com.example.bankandroid.RecyclerAdapters;

import android.annotation.SuppressLint;
import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ImageView;
import android.widget.TextView;
import android.widget.Toast;

import androidx.annotation.NonNull;
import androidx.core.content.ContextCompat;
import androidx.recyclerview.widget.RecyclerView;

import com.example.bankandroid.Models.Account;
import com.example.bankandroid.Models.AccountType;
import com.example.bankandroid.Models.Card;
import com.example.bankandroid.R;
import com.example.bankandroid.Utility.ApiInterface;
import com.example.bankandroid.Utility.Configurator;

import java.util.ArrayList;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class AccountRecyclerAdapter extends RecyclerView.Adapter<AccountRecyclerAdapter.AccountVH> {
    Context context;
    ArrayList<Account> list = new ArrayList<Account>();
    ApiInterface Interface;

    public AccountRecyclerAdapter(Context context, ArrayList<Account> list) {
        this.context = context;
        this.list = list;
    }

    @NonNull
    @Override
    public AccountVH onCreateViewHolder(@NonNull ViewGroup parent, int viewType) {
        View v = LayoutInflater.from(context).inflate(R.layout.account_item, parent, false);
        return new AccountRecyclerAdapter.AccountVH(v);
    }

    @Override
    public void onBindViewHolder(@NonNull AccountVH holder, @SuppressLint("RecyclerView") int position) {
        Account card = list.get(position);
        holder.txt_balance.setText(String.valueOf(card.get_Balance() + " руб."));
        holder.txt_percent.setText(String.valueOf(card.getPercent()) + " %");
        holder.accountIDtxt.setText("№ "+card.getIdAccount());

        if (card.isAccountDeleted() != true) {
            holder.txt_balance.setText("Ваш счёт заблокирован.");
            holder.txt_type.setText("---");
            holder.txt_percent.setText("");

            int icon = card.getTypeId();

            if (icon == '3') {
                holder.type.setImageDrawable(ContextCompat.getDrawable(context, R.drawable.credit));
            } else
                holder.type.setImageDrawable(ContextCompat.getDrawable(context, R.drawable.balance_wallet));
            }
            Interface = Configurator.buildRequest().create(ApiInterface.class);
            Call<AccountType> getAccountType = Interface.getAccountTypeby(card.getTypeId());
            getAccountType.enqueue(new Callback<AccountType>() {
                @Override
                public void onResponse(Call<AccountType> call, Response<AccountType> response) {
                    if (response.isSuccessful()){
                        holder.txt_type.setText(response.body().getNameAccountType());
                    }else {
                        Toast.makeText(context, "Ошибка клиента", Toast.LENGTH_SHORT).show();}
                }

                @Override
                public void onFailure(Call<AccountType> call, Throwable t) {
                    Toast.makeText(context, "Ошибка запроса", Toast.LENGTH_SHORT).show();
                }
            });
        }

    @Override
    public int getItemCount() {
        return list.size();
    }

    public static class AccountVH extends RecyclerView.ViewHolder
    {
        public TextView txt_balance, txt_type, txt_percent, accountIDtxt;
        public ImageView type;
        public AccountVH(@NonNull View itemView)
        {
            super(itemView);
            txt_percent = itemView.findViewById(R.id.accountPercenttxt);
            txt_balance = itemView.findViewById(R.id.accountBalancetxt);
            txt_type = itemView.findViewById(R.id.accountTypetxt);
            accountIDtxt = itemView.findViewById(R.id.accountIDtxt);
            type = itemView.findViewById(R.id.accountIcon);
        }
    }
}
