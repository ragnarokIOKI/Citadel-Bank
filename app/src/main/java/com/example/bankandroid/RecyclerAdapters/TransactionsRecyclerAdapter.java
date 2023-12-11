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
import com.example.bankandroid.Models.Transaction;
import com.example.bankandroid.Models.TransactionType;
import com.example.bankandroid.R;
import com.example.bankandroid.Utility.ApiInterface;
import com.example.bankandroid.Utility.Configurator;

import java.util.ArrayList;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class TransactionsRecyclerAdapter extends RecyclerView.Adapter<TransactionsRecyclerAdapter.TrVH>{
    Context context;
    ArrayList<Transaction> list = new ArrayList<Transaction>();
    ApiInterface Interface;

    public TransactionsRecyclerAdapter(Context context, ArrayList<Transaction> list) {
        this.context = context;
        this.list = list;
    }

    @NonNull
    @Override
    public TrVH onCreateViewHolder(@NonNull ViewGroup parent, int viewType) {
        View v = LayoutInflater.from(context).inflate(R.layout.transaction_card, parent, false);
        return new TransactionsRecyclerAdapter.TrVH(v);
    }

    @Override
    public void onBindViewHolder(@NonNull TrVH holder, @SuppressLint("RecyclerView") int position) {
        Transaction app = list.get(position);
        holder.name.setText(app.getNameTransaction());
        holder.summ.setText(String.valueOf(app.getSummTransaction()));
        holder.date.setText(String.valueOf(app.getDateTransaction()));

        Interface = Configurator.buildRequest().create(ApiInterface.class);
        Call<TransactionType> getAp = Interface.getTrTypeby(app.getTransactionTypeId());
        getAp.enqueue(new Callback<TransactionType>() {
            @Override
            public void onResponse(Call<TransactionType> call, Response<TransactionType> response) {
                if(response.isSuccessful()){
                    //разобраться с иконками
                }
            }

            @Override
            public void onFailure(Call<TransactionType> call, Throwable t) {
                Toast.makeText(context, t.getMessage(), Toast.LENGTH_SHORT).show();
            }
        });
    }

    @Override
    public int getItemCount() {
        return list.size();
    }

    public class TrVH extends RecyclerView.ViewHolder
    {
        public TextView name, summ, date;
        public TrVH(@NonNull View itemView)
        {
            super(itemView);
            name = itemView.findViewById(R.id.transactionNametxt);
            summ = itemView.findViewById(R.id.transactionAmounttxt);
            date = itemView.findViewById(R.id.transactionDatetxt);
        }
    }
}

