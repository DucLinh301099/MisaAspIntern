<template>
  <div class="summary-component">
    <div class="total-label">Tổng tiền</div>
    <div class="total-amount">{{ formattedTotalAmount }}</div>
  </div>
</template>

<script>
export default {
  name: "SummaryComponent",
  props: {
    totalAmount: {
      type: Number,
    },
  },
  computed: {
    formattedTotalAmount() {
      return this.totalAmount;
    },
  },
};
</script>

<style scoped>
.summary-component {
  display: flex;
  flex-direction: column;
  align-items: flex-end;
  padding: 0px 20px 0 20px;
}

.total-label {
  font-size: 16px;
  font-weight: bold;
  margin-bottom: 8px;
}

.total-amount {
  font-size: 36px;
  font-weight: bold;
}
</style>
