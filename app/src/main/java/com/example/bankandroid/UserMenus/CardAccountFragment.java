package com.example.bankandroid.UserMenus;

import static android.content.Context.MODE_PRIVATE;

import android.content.SharedPreferences;
import android.os.Bundle;

import androidx.fragment.app.FragmentManager;
import androidx.recyclerview.widget.LinearLayoutManager;
import androidx.recyclerview.widget.RecyclerView;

import androidx.fragment.app.Fragment;

import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.Toast;

import com.example.bankandroid.Models.Account;
import com.example.bankandroid.Models.Card;
import com.example.bankandroid.Models.Key;
import com.example.bankandroid.R;
import com.example.bankandroid.RecyclerAdapters.AccountRecyclerAdapter;
import com.example.bankandroid.RecyclerAdapters.CardRecyclerAdapter;
import com.example.bankandroid.Utility.ApiInterface;
import com.example.bankandroid.Utility.Configurator;

import java.util.ArrayList;
import java.util.concurrent.atomic.AtomicInteger;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class CardAccountFragment extends Fragment {
    ApiInterface Interface;
    public CardAccountFragment(int ke) {
        this.idUs = ke;
    }
    int idUs;
    RecyclerView rv, rvA;
    CardRecyclerAdapter adapter;
    AccountRecyclerAdapter adapterA;
    Button add;

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        View root = inflater.inflate(R.layout.fragment_card_account, container, false);

        rv = root.findViewById(R.id.rvCards);
        rvA = root.findViewById(R.id.rvAccounts);
        add = root.findViewById(R.id.addAccount);

        Interface = Configurator.buildRequest().create(ApiInterface.class);

        SharedPreferences sharedPreferences = getActivity().getSharedPreferences("myPrefs", MODE_PRIVATE);
        String token = sharedPreferences.getString("token", "");

        rv.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Call<Key> refresh = Interface.refresh(token);
                refresh.enqueue(new Callback<Key>() {
                    @Override
                    public void onResponse(Call<Key> call, Response<Key> response) {
                        if (response.isSuccessful()) {
                            Toast.makeText(getContext(), "Токен продлён: " + response.body().getAccess_token(), Toast.LENGTH_SHORT).show();
                            SharedPreferences sharedPreferences = getActivity().getSharedPreferences("myPrefs", MODE_PRIVATE);
                            SharedPreferences.Editor editor = sharedPreferences.edit();
                            editor.putString("token", response.body().getAccess_token());
                            editor.apply();
                        }
                    }
                    @Override
                    public void onFailure(Call<Key> call, Throwable t) {
                        Toast.makeText(getContext(), t.getMessage(), Toast.LENGTH_SHORT).show();
                    }
                });
            }
        });

        Call<ArrayList<Account>> getAccounts = Interface.getAccount(idUs, true);
        getAccounts.enqueue(new Callback<ArrayList<Account>>() {
            @Override
            public void onResponse(Call<ArrayList<Account>> call, Response<ArrayList<Account>> response) {
                if (response.isSuccessful()) {
                    rvA.setLayoutManager(new LinearLayoutManager(getContext()));
                    rvA.setHasFixedSize(true);
                    ArrayList<Account> listGame = response.body();
                    adapterA = new AccountRecyclerAdapter(getContext(), listGame);
                    rvA.setAdapter(adapterA);

                    ArrayList<Card> allCards = new ArrayList<>();
                    rv.setLayoutManager(new LinearLayoutManager(getContext(), LinearLayoutManager.HORIZONTAL, false));
                    rv.setHasFixedSize(true);
                    adapter = new CardRecyclerAdapter(getContext(), allCards);
                    rv.setAdapter(adapter);

                    int totalRequests = listGame.size();
                    AtomicInteger completedRequests = new AtomicInteger(0);

                    for (Account account : listGame) {
                        String accountNumber = account.getIdAccount();
                        Call<ArrayList<Card>> getCards = Interface.getCard(accountNumber, null);
                        getCards.enqueue(new Callback<ArrayList<Card>>() {

                            @Override
                            public void onResponse(Call<ArrayList<Card>> call, Response<ArrayList<Card>> response) {
                                if(response.isSuccessful()){
                                    ArrayList<Card> listCards = response.body();
                                    for (Card card : listCards) {
                                        if (!containsCard(allCards, card)) {
                                            allCards.add(card);
                                        }
                                    }
                                    adapter.notifyDataSetChanged();
                                }else{
                                    Card noCard = new Card("У вас ещё нет банковской карты на счету " + accountNumber,"XXXXXXXX XXXXXXXX", "XX/XX", "---", "0", true, "-");
                                    allCards.add(noCard);
                                    adapter.notifyDataSetChanged();
                                }

                                if (completedRequests.incrementAndGet() == totalRequests) {
                                    adapter.notifyDataSetChanged();
                                }
                            }

                            @Override
                            public void onFailure(Call<ArrayList<Card>> call, Throwable t) {
                                completedRequests.incrementAndGet();
                                if (completedRequests.get() == totalRequests) {
                                    adapter.notifyDataSetChanged();
                                }
                                Toast.makeText(getContext(), t.getMessage(), Toast.LENGTH_SHORT).show();
                            }
                        });
                    }
                }
            }

            @Override
            public void onFailure(Call<ArrayList<Account>> call, Throwable t) {
                Toast.makeText(getContext(), t.getMessage(), Toast.LENGTH_SHORT).show();
            }
        });

        add.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                FragmentManager manager = getParentFragmentManager();
                AccountApplicationAddFragment myDialogFragment = new AccountApplicationAddFragment();
                myDialogFragment.show(manager, "addAccApplication");
            }
        });
        return root;
    }

    private boolean containsCard(ArrayList<Card> list, Card card){
        for (Card c : list){
            if (c.getIdCard().equals(card.getIdCard())){
                return true;
            }
        }
        return false;
    }
}