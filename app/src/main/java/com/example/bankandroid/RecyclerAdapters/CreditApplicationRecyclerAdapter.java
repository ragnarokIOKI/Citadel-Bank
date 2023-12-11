package com.example.bankandroid.RecyclerAdapters;

import android.annotation.SuppressLint;
import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.TextView;
import android.widget.Toast;

import androidx.annotation.NonNull;
import androidx.recyclerview.widget.RecyclerView;

import com.example.bankandroid.Models.ApplicationStatus;
import com.example.bankandroid.Models.CreditApplication;
import com.example.bankandroid.R;
import com.example.bankandroid.Utility.ApiInterface;
import com.example.bankandroid.Utility.Configurator;

import java.util.ArrayList;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class CreditApplicationRecyclerAdapter extends RecyclerView.Adapter<CreditApplicationRecyclerAdapter.CredApVH>{
    Context context;
    ArrayList<CreditApplication> list = new ArrayList<CreditApplication>();
    ApiInterface Interface;

    public CreditApplicationRecyclerAdapter(Context context, ArrayList<CreditApplication> list) {
        this.context = context;
        this.list = list;
    }

    @NonNull
    @Override
    public CredApVH onCreateViewHolder(@NonNull ViewGroup parent, int viewType) {
        View v = LayoutInflater.from(context).inflate(R.layout.creditapplication_item, parent, false);
        return new CreditApplicationRecyclerAdapter.CredApVH(v);
    }

    @Override
    public void onBindViewHolder(@NonNull CredApVH holder, @SuppressLint("RecyclerView") int position) {
        CreditApplication app = list.get(position);
        holder.idCreditApplication.setText("Заявка №"+app.getIdCreditApplication());
        holder.applicationAmount.setText("Сумма: "+String.valueOf(app.getApplicationAmount()));
        holder.creditDesiredPercentage.setText("Ставка: "+String.valueOf(app.getCreditDesiredPercentage()));

        Interface = Configurator.buildRequest().create(ApiInterface.class);
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

    public class CredApVH extends RecyclerView.ViewHolder
    {
        public TextView idCreditApplication, applicationAmount, creditDesiredPercentage, statusId;
        public CredApVH(@NonNull View itemView)
        {
            super(itemView);
            creditDesiredPercentage = itemView.findViewById(R.id.creditDesiredPercentage);
            statusId = itemView.findViewById(R.id.statusId);
            idCreditApplication = itemView.findViewById(R.id.idCreditApplication);
            applicationAmount = itemView.findViewById(R.id.applicationAmount);
        }
    }
}
