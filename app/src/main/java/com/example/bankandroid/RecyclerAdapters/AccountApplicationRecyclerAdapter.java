package com.example.bankandroid.RecyclerAdapters;

import android.annotation.SuppressLint;
import android.content.Context;
import android.support.annotation.NonNull;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.TextView;
import android.widget.Toast;

import androidx.recyclerview.widget.RecyclerView;

import com.example.bankandroid.Models.AccountApplication;
import com.example.bankandroid.Models.AccountType;
import com.example.bankandroid.Models.ApplicationStatus;
import com.example.bankandroid.R;
import com.example.bankandroid.Utility.ApiInterface;
import com.example.bankandroid.Utility.Configurator;

import java.util.ArrayList;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class AccountApplicationRecyclerAdapter extends RecyclerView.Adapter<AccountApplicationRecyclerAdapter.AccApVH>{
    Context context;
    ArrayList<AccountApplication> list = new ArrayList<AccountApplication>();
    ApiInterface Interface;

    public AccountApplicationRecyclerAdapter(Context context, ArrayList<AccountApplication> list) {
        this.context = context;
        this.list = list;
    }

    @NonNull
    @Override
    public AccApVH onCreateViewHolder(@NonNull ViewGroup parent, int viewType) {
        View v = LayoutInflater.from(context).inflate(R.layout.creditapplication_item, parent, false);
        return new AccountApplicationRecyclerAdapter.AccApVH(v);
    }

    @Override
    public void onBindViewHolder(@NonNull AccApVH holder, @SuppressLint("RecyclerView") int position) {
        AccountApplication app = list.get(position);

        if (list.size() == 0){
            holder.idAccountApplication.setText("Не найдено элементов, удовлетворяющих настройкам поиска");
            holder.AccountDesiredPercentage.setText("");
            holder.applicationAmount.setText("");
            holder.statusId.setText("");
        }
        holder.idAccountApplication.setText("Заявка №"+app.getIdAccountApplication());
        holder.AccountDesiredPercentage.setText("Ставка: "+String.valueOf(app.getAccountDesiredPercentage()));

        Interface = Configurator.buildRequest().create(ApiInterface.class);
        Call<AccountType> getAccountTypeby = Interface.getAccountTypeby(app.getTypeAccountId());
        getAccountTypeby.enqueue(new Callback<AccountType>() {
            @Override
            public void onResponse(Call<AccountType> call, Response<AccountType> response) {
                holder.applicationAmount.setText("Вид счёта: "+String.valueOf(response.body().getNameAccountType()));
            }

            @Override
            public void onFailure(Call<AccountType> call, Throwable t) {
                Toast.makeText(context, t.getMessage(), Toast.LENGTH_SHORT).show();
            }
        });
        Call<ApplicationStatus> getAp = Interface.getAppStatby(app.getStatusId());
        getAp.enqueue(new Callback<ApplicationStatus>() {
            @Override
            public void onResponse(Call<ApplicationStatus> call, Response<ApplicationStatus> response) {
                if(response.isSuccessful()){
                    holder.statusId.setText("Статус: " + response.body().getNameStatus());
                }
            }

            @Override
            public void onFailure(Call<ApplicationStatus> call, Throwable t) {
                Toast.makeText(context, t.getMessage(), Toast.LENGTH_SHORT).show();
            }
        });
    }

    @Override
    public int getItemCount() {
        return list.size();
    }

    public class AccApVH extends RecyclerView.ViewHolder
    {
        public TextView idAccountApplication, applicationAmount, AccountDesiredPercentage, statusId;
        public AccApVH(@NonNull View itemView)
        {
            super(itemView);
            AccountDesiredPercentage = itemView.findViewById(R.id.creditDesiredPercentage);
            statusId = itemView.findViewById(R.id.statusId);
            idAccountApplication = itemView.findViewById(R.id.idCreditApplication);
            applicationAmount = itemView.findViewById(R.id.applicationAmount);
        }
    }
}

