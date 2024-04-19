package com.ProjectMana.ProjectManagementSpring.repo;

import com.ProjectMana.ProjectManagementSpring.enteties.UserActivity;
import com.ProjectMana.ProjectManagementSpring.enteties.project;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.stereotype.Component;

import java.util.List;

@Component

public interface ActivityRepo extends JpaRepository<UserActivity,Integer> {
  @Query("SELECT ua FROM UserActivity ua WHERE ua.user.id = :userId AND ua.task.no = :taskId")
  List<UserActivity> findByUserIdAndTaskId(Integer userId, Integer taskId);
}
