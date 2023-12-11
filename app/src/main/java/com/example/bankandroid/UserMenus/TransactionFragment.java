package com.example.bankandroid.UserMenus;

import static android.content.Context.MODE_PRIVATE;

import android.content.SharedPreferences;
import android.graphics.Color;
import android.os.Bundle;

import androidx.fragment.app.Fragment;
import androidx.fragment.app.FragmentManager;
import androidx.fragment.app.FragmentTransaction;
import androidx.recyclerview.widget.LinearLayoutManager;
import androidx.recyclerview.widget.RecyclerView;

import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.ImageView;
import android.widget.Spinner;
import android.widget.Toast;

import com.example.bankandroid.Models.Account;
import com.example.bankandroid.Models.Transaction;
import com.example.bankandroid.R;
import com.example.bankandroid.RecyclerAdapters.TransactionsRecyclerAdapter;
import com.example.bankandroid.Utility.ApiInterface;
import com.example.bankandroid.Utility.Configurator;
import com.github.mikephil.charting.charts.PieChart;
import com.github.mikephil.charting.data.PieData;
import com.github.mikephil.charting.data.PieDataSet;
import com.github.mikephil.charting.data.PieEntry;
import com.github.mikephil.charting.formatter.PercentFormatter;
import com.github.mikephil.charting.utils.ColorTemplate;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class TransactionFragment extends Fragment {
    Spinner spinnerAcc;
    public Button topuphistory, withdrawlhistory;
    public PieChart pieChart;
    RecyclerView rv;
    TransactionsRecyclerAdapter rvA;
    ImageView down;
    ApiInterface Interface;
    int idUser;
    int typetransactionID = 1;
    Call<ArrayList<Transaction>> transactionListCall;
    ArrayList<String> array = new ArrayList<String>();
    ArrayList<Transaction> transactions;
    String s = "0";

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        View root = inflater.inflate(R.layout.fragment_transaction, container, false);

        pieChart = root.findViewById(R.id.chart);
        down = root.findViewById(R.id.downTran);
        spinnerAcc = root.findViewById(R.id.spinnerAcc);
        rv = root.findViewById(R.id.TranRV);
        topuphistory = root.findViewById(R.id.topuphistory);
        withdrawlhistory = root.findViewById(R.id.withdrawlhistory);

        Interface = Configurator.buildRequest().create(ApiInterface.class);

        SharedPreferences sharedPreferences = getActivity().getSharedPreferences("myPrefs", MODE_PRIVATE);
        idUser = sharedPreferences.getInt("id", 0);

        Call<ArrayList<Account>> getAcc = Interface.getAccount(idUser, true);
        getAcc.enqueue(new Callback<ArrayList<Account>>() {
            @Override
            public void onResponse(Call<ArrayList<Account>> call, Response<ArrayList<Account>> response) {
                if (response.isSuccessful()){
                    if (response.body().isEmpty()) {
                        Toast.makeText(getContext(), "Нет транзакций для отображения", Toast.LENGTH_SHORT).show();
                        s = "0";
                    } else {
                        for (int i = response.body().size() - 1; i >= 0; i--){
                            array.add(response.body().get(i).getIdAccount());
                        }

                        ArrayAdapter<String> adapter = new ArrayAdapter<String>(getContext(), android.R.layout.simple_spinner_item, array);
                        adapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
                        spinnerAcc.setAdapter(adapter);
                    }
                } else {
                    Toast.makeText(getContext(), "Нет данных.", Toast.LENGTH_SHORT).show();
                }
            }

            @Override
            public void onFailure(Call<ArrayList<Account>> call, Throwable t) {
                Toast.makeText(getContext(), "Ошибка клиента.", Toast.LENGTH_SHORT).show();
            }
        });

        topuphistory.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                typetransactionID = 1;
                transactionListCall = Interface.getTransactions(s, typetransactionID, true);
                executeTransactionListCall();
            }
        });

        withdrawlhistory.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                typetransactionID = 2;
                transactionListCall = Interface.getTransactions(s, typetransactionID, true);
                executeTransactionListCall();
            }
        });

        spinnerAcc.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> parent, View view, int position, long id) {
                s = spinnerAcc.getSelectedItem().toString();
                transactionListCall = Interface.getTransactions(s, typetransactionID, true);
                executeTransactionListCall();
            }

            @Override
            public void onNothingSelected(AdapterView<?> parent) {

            }
        });

        down.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                FragmentManager fragmentManager = getActivity().getSupportFragmentManager();
                UserFragment newFragment = new UserFragment();
                FragmentTransaction fragmentTransaction = fragmentManager.beginTransaction();
                fragmentTransaction.setCustomAnimations( R.anim.slidedown,0);
                fragmentTransaction.replace(R.id.fragment_layout, newFragment);
                fragmentTransaction.addToBackStack(null);
                fragmentTransaction.commit();
            }
        });
        return root;
    }

    private void updatePieChart(List<Transaction> transactions) {
        Map<String, Float> transactionSums = new HashMap<>();

        for (Transaction transaction : transactions) {
            String name = transaction.getNameTransaction();
            double sum = transaction.getSummTransaction();

            if (transactionSums.containsKey(name)) {
                transactionSums.put(name, transactionSums.get(name) + (float) sum);
            } else {
                transactionSums.put(name, (float) sum);
            }
        }

        List<PieEntry> pieEntries = new ArrayList<>();
        for (Map.Entry<String, Float> entry : transactionSums.entrySet()) {
            pieEntries.add(new PieEntry(entry.getValue(), entry.getKey()));
        }

        PieDataSet dataSet = new PieDataSet(pieEntries, "Транзакции");
        dataSet.setDrawValues(true);
        dataSet.setValueFormatter(new PercentFormatter(pieChart));
        dataSet.setValueTextSize(12f);
        dataSet.setColors(ColorTemplate.VORDIPLOM_COLORS);
        dataSet.setValueTextColor(Color.BLACK);
        dataSet.setValueTextSize(12f);
        dataSet.setLabel("");

        PieData pieData = new PieData(dataSet);
        pieChart.setData(pieData);
        pieChart.notifyDataSetChanged();
        pieChart.invalidate();

    }

    private void executeTransactionListCall() {
        transactionListCall.enqueue(new Callback<ArrayList<Transaction>>() {
            @Override
            public void onResponse(Call<ArrayList<Transaction>> call, Response<ArrayList<Transaction>> response) {
                if (response.isSuccessful()) {
                    transactions = response.body();

                    if (transactions.isEmpty()) {
                        Toast.makeText(getContext(), "Нет транзакций для отображения", Toast.LENGTH_SHORT).show();
                    } else {
                        updatePieChart(transactions);

                        rv.setLayoutManager(new LinearLayoutManager(getContext()));
                        rv.setHasFixedSize(true);
                        ArrayList<Transaction> listGame = response.body();
                        rvA = new TransactionsRecyclerAdapter(getContext(), listGame);
                        rv.setAdapter(rvA);
                    }
                } else {
                    Toast.makeText(getContext(), "Нет транзакций для отображения", Toast.LENGTH_SHORT).show();
                }
            }

            @Override
            public void onFailure(Call<ArrayList<Transaction>> call, Throwable t) {
                Toast.makeText(getContext(), t.getMessage() , Toast.LENGTH_SHORT).show();
            }
        });
    }
}