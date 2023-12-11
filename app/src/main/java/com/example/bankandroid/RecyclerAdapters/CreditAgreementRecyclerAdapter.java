package com.example.bankandroid.RecyclerAdapters;

import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.TextView;
import android.widget.Toast;

import androidx.annotation.NonNull;
import androidx.recyclerview.widget.RecyclerView;

import com.example.bankandroid.Models.CreditAgreement;
import com.example.bankandroid.Models.CreditApplication;
import com.example.bankandroid.R;
import com.example.bankandroid.Utility.ApiInterface;
import com.example.bankandroid.Utility.Configurator;

import java.time.LocalDateTime;
import java.util.ArrayList;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class CreditAgreementRecyclerAdapter extends RecyclerView.Adapter<CreditAgreementRecyclerAdapter.CredAgVH>{
    Context context;
    ArrayList<CreditAgreement> list = new ArrayList<CreditAgreement>();
    ApiInterface Interface;
    public CreditAgreementRecyclerAdapter(Context context, ArrayList<CreditAgreement> list) {
        this.context = context;
        this.list = list;
    }

    @NonNull
    @Override
    public CredAgVH onCreateViewHolder(@NonNull ViewGroup parent, int viewType) {
        View v = LayoutInflater.from(context).inflate(R.layout.credit_agreement_item, parent, false);
        return new CreditAgreementRecyclerAdapter.CredAgVH(v);
    }

    @Override
    public void onBindViewHolder(@NonNull CredAgVH holder, int position) {
        CreditAgreement ag = list.get(position);
        holder.IdCreditAgreement.setText("Договор №"+ag.getIdCreditAgreement());
        holder.creditRate.setText("Ставка: "+String.valueOf(ag.getCreditRate()));
        holder.date1.setText("Дата открытия договора: \n"+ag.getDateDrawing());
        holder.date2.setText("Дата закрытия договора \n"+ ag.getDateTermination());

        Interface = Configurator.buildRequest().create(ApiInterface.class);
        Call<CreditApplication> getAp = Interface.getCreditApplicationby(Integer.parseInt(ag.getCreditApplicationId()));
        getAp.enqueue(new Callback<CreditApplication>() {
            @Override
            public void onResponse(Call<CreditApplication> call, Response<CreditApplication> response) {
                if(response.isSuccessful()){
                    holder.ApplicId.setText("Заявка №"+response.body().getIdCreditApplication());
                }
            }

            @Override
            public void onFailure(Call<CreditApplication> call, Throwable t) {
                Toast.makeText(context, t.getMessage(), Toast.LENGTH_SHORT).show();
            }
        });
    }

    @Override
    public int getItemCount() {
        return list.size();
    }

    public class CredAgVH extends RecyclerView.ViewHolder {
        public TextView date1, date2, IdCreditAgreement, creditRate, ApplicId;

        public CredAgVH(@NonNull View itemView) {
            super(itemView);
            IdCreditAgreement = itemView.findViewById(R.id.IdCreditAgreement);
            creditRate = itemView.findViewById(R.id.creditRate);
            date1 = itemView.findViewById(R.id.date1);
            date2 = itemView.findViewById(R.id.date2);
            ApplicId = itemView.findViewById(R.id.ApplicId);
        }
    }
}
