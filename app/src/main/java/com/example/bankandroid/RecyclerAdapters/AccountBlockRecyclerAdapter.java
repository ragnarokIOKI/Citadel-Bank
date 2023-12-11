package com.example.bankandroid.RecyclerAdapters;

import android.annotation.SuppressLint;
import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.CheckBox;
import android.widget.CompoundButton;
import android.widget.TextView;

import androidx.annotation.NonNull;
import androidx.recyclerview.widget.RecyclerView;

import com.example.bankandroid.Models.Account;
import com.example.bankandroid.R;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.Map;

public class AccountBlockRecyclerAdapter extends RecyclerView.Adapter<AccountBlockRecyclerAdapter.AccBlockVH>{
    Context context;
    public ArrayList<Account> list = new ArrayList<Account>();
    public Map<Integer, Boolean> checked = new HashMap<>();

    public AccountBlockRecyclerAdapter(Context context, ArrayList<Account> list) {
        this.context = context;
        this.list = list;
    }

    @NonNull
    @Override
    public AccBlockVH onCreateViewHolder(@NonNull ViewGroup parent, int viewType) {
        View v = LayoutInflater.from(context).inflate(R.layout.block_cardaccount_item, parent, false);
        return new AccountBlockRecyclerAdapter.AccBlockVH(v);
    }

    @Override
    public void onBindViewHolder(@NonNull AccountBlockRecyclerAdapter.AccBlockVH holder, @SuppressLint("RecyclerView") int position) {
        Account card = list.get(position);
        holder.txt_number.setText(String.valueOf(card.getIdAccount()));
        holder.checkboxCard.setOnCheckedChangeListener(new CompoundButton.OnCheckedChangeListener() {
            @Override
            public void onCheckedChanged(CompoundButton buttonView, boolean isChecked) {
                checked.put(position, isChecked);
            }
        });
    }

    @Override
    public int getItemCount() {
        return list.size();
    }


    public static class AccBlockVH extends RecyclerView.ViewHolder
    {
        public TextView txt_number;
        public CheckBox checkboxCard;
        public AccBlockVH(@NonNull View itemView)
        {
            super(itemView);
            txt_number = itemView.findViewById(R.id.numberAccCard);
            checkboxCard = itemView.findViewById(R.id.checkboxCard);
        }
    }
}
