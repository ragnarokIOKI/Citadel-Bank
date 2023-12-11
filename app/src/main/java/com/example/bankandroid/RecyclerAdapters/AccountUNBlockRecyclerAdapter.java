package com.example.bankandroid.RecyclerAdapters;

import android.annotation.SuppressLint;
import android.content.Context;
import android.support.annotation.NonNull;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.CheckBox;
import android.widget.CompoundButton;
import android.widget.TextView;

import androidx.recyclerview.widget.RecyclerView;

import com.example.bankandroid.Models.Account;
import com.example.bankandroid.Models.Card;
import com.example.bankandroid.R;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.Map;

public class AccountUNBlockRecyclerAdapter extends RecyclerView.Adapter<AccountUNBlockRecyclerAdapter.AccountUNBlockVH>{
    Context context;
    public ArrayList<Account> list = new ArrayList<Account>();
    public Map<Integer, Boolean> checked = new HashMap<>();

    public AccountUNBlockRecyclerAdapter(Context context, ArrayList<Account> list) {
        this.context = context;
        this.list = list;
    }

    @NonNull
    @Override
    public AccountUNBlockVH onCreateViewHolder(@NonNull ViewGroup parent, int viewType) {
        View v = LayoutInflater.from(context).inflate(R.layout.block_cardaccount_item, parent, false);
        return new AccountUNBlockRecyclerAdapter.AccountUNBlockVH(v);
    }

    @Override
    public void onBindViewHolder(@NonNull AccountUNBlockRecyclerAdapter.AccountUNBlockVH holder,  @SuppressLint("RecyclerView") int position) {
        Account Account = list.get(position);
        holder.txt_number.setText(Account.getIdAccount());

        holder.checkbox.setOnCheckedChangeListener(new CompoundButton.OnCheckedChangeListener() {
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

    public static class AccountUNBlockVH extends RecyclerView.ViewHolder {
        public TextView txt_number;
        public CheckBox checkbox;

        public AccountUNBlockVH(@NonNull View itemView) {
            super(itemView);
            txt_number = itemView.findViewById(R.id.numberAccCard);
            checkbox = itemView.findViewById(R.id.checkboxCard);
        }
    }
}
