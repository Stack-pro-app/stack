package com.ProjectMana.ProjectManagementSpring.repo;

import com.ProjectMana.ProjectManagementSpring.enteties.Task;
import com.ProjectMana.ProjectManagementSpring.enteties.UserT;
import com.ProjectMana.ProjectManagementSpring.enteties.WorkSpace;
import com.ProjectMana.ProjectManagementSpring.enteties.project;
import jakarta.persistence.criteria.CriteriaBuilder;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Component;

import java.util.List;

@Component
public interface UserRepo extends JpaRepository<UserT, Integer> {

  UserT findByUserName (String name);

  UserT findByAuthId(String authId);
  List<UserT> findAllByTasksIn(List<Task> l );

}
