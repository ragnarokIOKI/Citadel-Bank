package com.example.bankandroid.UserMenus;

import android.os.Bundle;
import androidx.fragment.app.Fragment;
import androidx.fragment.app.FragmentManager;
import androidx.fragment.app.FragmentTransaction;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.AdapterView;
import android.widget.ListView;
import android.widget.Toast;
import com.example.bankandroid.R;
import com.example.bankandroid.RecyclerAdapters.CustomListAdapter;
import com.example.bankandroid.SettingsFragment;
import java.util.ArrayList;
import java.util.List;

public class UserFragment extends Fragment {

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
        View rootView = inflater.inflate(R.layout.fragment_user, container, false);

        ListView listView = rootView.findViewById(R.id.list_viewUser);

        List<String> itemList = new ArrayList<>();
        itemList.add("Анализ расходов/доходов");
        itemList.add("Проверить статус заявки");
        itemList.add("Подать заявку на кредит");
        itemList.add("Проверить кредитную историю");
        itemList.add("Разблокировать/заблокировать счёт");
        itemList.add("Разблокировать/заблокировать банковскую карту");
        itemList.add("Вычислить переплату по кредиту");
        itemList.add("Настройки");

        CustomListAdapter adapter = new CustomListAdapter(getActivity(), itemList);
        listView.setAdapter(adapter);

        listView.setOnItemClickListener((parent, view, position, id) -> {
            FragmentManager fragmentManager;
            Fragment newFragment;
            FragmentTransaction fragmentTransaction;

            switch (position) {
                case 0:
                case 1:
                case 3:
                case 4:
                case 5:
                case 7:
                    fragmentManager = getActivity().getSupportFragmentManager();
                    newFragment = getFragmentByPosition(position);
                    fragmentTransaction = fragmentManager.beginTransaction();
                    fragmentTransaction.setCustomAnimations(R.anim.slideup, R.anim.fadeout);
                    fragmentTransaction.replace(R.id.fragment_layout, newFragment);
                    fragmentTransaction.addToBackStack(null);
                    fragmentTransaction.commit();
                    break;

                case 2:
                    FragmentManager manager = getParentFragmentManager();
                    CreditApplicationAddFragment myDialogFragment = new CreditApplicationAddFragment();
                    myDialogFragment.show(manager, "addCreditApplication");
                    break;

                case 6:
                    FragmentManager manager2 = getParentFragmentManager();
                    OverpaymentFragment myDialogFragment2 = new OverpaymentFragment();
                    myDialogFragment2.show(manager2, "overpaymentCalculate");
                    break;

                default:
                    Toast.makeText(getContext(), "Не найдено.", Toast.LENGTH_SHORT).show();
                    break;
            }
        });

        return rootView;
    }

    private Fragment getFragmentByPosition(int position) {
        switch (position) {
            case 0:
                return new TransactionFragment();
            case 1:
                return new AccountApplicationCheckFragment();
            case 3:
                return new CreditHistoryFragment();
            case 4:
                return new BlockAccountFragment();
            case 5:
                return new BlockCardFragment();
            case 7:
                return new SettingsFragment();
            default:
                return null;
        }
    }
}